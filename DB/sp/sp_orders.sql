

CREATE PROCEDURE sp_orders

@i_operation_type	VARCHAR(50) = NULL,
@i_description		VARCHAR(255) = NULL,
@i_weight_order		FLOAT = 0,
@i_number_products	int = 0,
@i_amount_total		FLOAT = 0,
@i_status			VARCHAR(20) = NULL,
@i_order_id			int = 0

AS

IF(@i_operation_type = 'REGISTER_ORDER')
BEGIN
	BEGIN TRAN
		insert into agex_orders
		(
			DESCRIPTION,			WEIGHT_ORDER,				NUMBER_PRODUCTS,		AMOUNT_TOTAL,
			CREATE_DATETIME,		STATUS
		)
		values
		(
			@i_description,			@i_weight_order,			@i_number_products,		@i_amount_total,
			GETDATE(),				'A'
		)

		IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se pudo realizar el registro de la orden.', 16, 1)
			RETURN 1
		END
	COMMIT TRAN
END
IF(@i_operation_type = 'UPDATE_STATUS_ORDER')
BEGIN
	IF NOT EXISTS(SELECT TOP 1 * FROM agex_orders where ORDER_ID = @i_order_id)
	BEGIN
		RAISERROR('No se encuentra la orden a buscar', 16, 1)
		RETURN 1
	END

	BEGIN TRAN
		update	agex_orders
		set		STATUS = @i_status
		where	ORDER_ID = @i_order_id

		IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se pudo realizar la actualizacion del estado de la orden.', 16, 1)
			RETURN 1
		END
	COMMIT TRAN
END
IF(@i_operation_type = 'DELETE_ORDER')
BEGIN
	IF NOT EXISTS(SELECT TOP 1 * FROM agex_orders where ORDER_ID = @i_order_id)
	BEGIN
		RAISERROR('No se encuentra la orden a buscar', 16, 1)
		RETURN 1
	END

	BEGIN TRAN
		UPDATE	agex_orders
		set		STATUS = 'INACTIVE'
		WHERE	ORDER_ID = @i_order_id

		IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se pudo eliminar la orden.', 16, 1)
			RETURN 1
		END
	COMMIT TRAN
END
IF(@i_operation_type = 'GET_ORDERS')
BEGIN
	select	ORDER_ID,
			DESCRIPTION,
			WEIGHT_ORDER,
			NUMBER_PRODUCTS,
			AMOUNT_TOTAL,
			STATUS
	from	agex_orders

	IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
	BEGIN
		RAISERROR('No se encontraron registros de ordenes.', 16, 1)
		RETURN 1
	END
END
IF(@i_operation_type = 'GET_STATUS_ORDER')
BEGIN
	IF NOT EXISTS(SELECT TOP 1 * FROM agex_orders where ORDER_ID = @i_order_id)
	BEGIN
		RAISERROR('No se encuentra la orden a buscar', 16, 1)
		RETURN 1
	END

	select	ORDER_ID,
			DESCRIPTION,
			WEIGHT_ORDER,
			NUMBER_PRODUCTS,
			AMOUNT_TOTAL,
			STATUS
	from	agex_orders
	where	ORDER_ID = @i_order_id

	IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
	BEGIN
		RAISERROR('No se encontraron registros de la orden buscada.', 16, 1)
		RETURN 1
	END
END
