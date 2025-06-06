

CREATE PROCEDURE sp_employee

@i_operation_type	varchar(50) = null,
@i_employee_name	varchar(50) = null,
@i_employee_last_name varchar(50) = null,
@i_age				int = 0,
@i_birthday			varchar(10) = null,
@i_gender			varchar(1) = null,
@i_dpi				varchar(13) = null,
@i_nit				varchar(15) = null,
@i_igss				varchar(13) = null,
@i_irtra			varchar(13) = null,
@i_passport			varchar(13) = null,
@i_workstation_id	int = 0,
@i_address_line_1	varchar(max) = null,
@i_address_line_2	varchar(max) = null,
@i_email			varchar(100) = null,
@i_phone			varchar(100) = null,
@i_employee_id		int = 0

AS

IF(@i_operation_type = 'REGISTER_EMPLOYEE')
BEGIN
	IF EXISTS(SELECT TOP 1 * FROM agex_employee WHERE EMPLOYEE_DPI = @i_dpi)
	BEGIN
		RAISERROR('El empleado ya se encuentra registrado.', 16, 1)
		RETURN 1
	END

	BEGIN TRAN
		insert into agex_employee
		(
			EMPLOYEE_NAME,			EMPLOYEE_LAST_NAME,			AGE,			DATE_BIRTHDAY,
			GENDER,					EMPLOYEE_DPI,				NIT,			IGSS,
			IRTRA,					PASSPORT,					WORKSTATION_ID,	CREATE_DATETIME,
			STATUS
		)
		values
		(
			@i_employee_name,		@i_employee_last_name,		@i_age,			@i_birthday,
			@i_gender,				@i_dpi,						@i_nit,			@i_igss,
			@i_irtra,				@i_passport,				@i_workstation_id,	GETDATE(),
			'A'
		)

		IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se logro registrar el empleado.', 16, 1)
			RETURN 1
		END

		insert into agex_employee_address
		(
			EMPLOYEE_DPI,			ADDRESS_LINE_1,				ADDRESS_LINE_2,			COUNTRY_ID,
			STATE_ID,				CREATE_DATETIME,			STATUS
		)
		values
		(
			@i_dpi,					@i_address_line_1,			@i_address_line_2,		'1',
			'1',					GETDATE(),					'A'
		)

		IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se logro registrar la direccion.', 16, 1)
			RETURN 1
		END

		insert into agex_employee_email
		(
			EMPLOYEE_DPI,				EMAIL,					CREATE_DATETIME,			STATUS					 
		)
		values
		(
			@i_dpi,						@i_email,				GETDATE(),					'A'
		)

		IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se logro registrar el email.', 16, 1)
			RETURN 1
		END

		insert into agex_employee_phone
		(
			EMPLOYEE_DPI,				EMPLOYEE_PHONE,				CREATE_DATETIME,		STATUS
		)
		values
		(
			@i_dpi,						@i_phone,					GETDATE(),				'A'
		)

		IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se logro registrar el numero telefonico.', 16, 1)
			RETURN 1
		END
	COMMIT TRAN
END
IF(@i_operation_type = 'UPDATE_EMPLOYEE')
BEGIN
	IF NOT EXISTS(SELECT TOP 1 * FROM agex_employee WHERE EMPLOYEE_DPI = @i_dpi)
	BEGIN
		RAISERROR('El empleado a actualizar no existe.', 16, 1)
		RETURN 1
	END

	BEGIN TRAN
		update	agex_employee
		set		EMPLOYEE_NAME = @i_employee_name,
				EMPLOYEE_LAST_NAME = @i_employee_last_name,
				AGE	= @i_age,
				WORKSTATION_ID = @i_workstation_id,
				UPDATE_DATETIME = GETDATE()
		where	EMPLOYEE_DPI = @i_dpi

		IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se logro actualizar el registro.', 16, 1)
			RETURN 1
		END

		update	agex_employee_address
		set		ADDRESS_LINE_1 = @i_address_line_1,
				ADDRESS_LINE_2 = @i_address_line_2,
				UPDATE_DATETIME = GETDATE()
		where	EMPLOYEE_DPI = @i_dpi

		IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se logro actualizar la dirección.', 16, 1)
			RETURN 1
		END

		update	agex_employee_email
		set		EMAIL = @i_email,
				UPDATE_DATETIME = GETDATE()
		where	EMPLOYEE_DPI = @i_dpi

		IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se logro actualizar el email.', 16, 1)
			RETURN 1
		END

		update	agex_employee_phone
		set		EMPLOYEE_PHONE = @i_phone,
				UPDATE_DATETIME = GETDATE()
		where	EMPLOYEE_DPI = @i_dpi

		IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se logro actualizar el numero telefonico.', 16, 1)
			RETURN 1
		END
	COMMIT TRAN
END
IF(@i_operation_type = 'DELETE_EMPLOYEE')
BEGIN
	IF NOT EXISTS(SELECT TOP 1 * FROM agex_employee WHERE EMPLOYEE_DPI = @i_dpi)
	BEGIN
		RAISERROR('El empleado a eliminar no existe.', 16, 1)
		RETURN 1
	END

	BEGIN TRAN
		update	agex_employee
		set		STATUS = 'I'
		where	EMPLOYEE_DPI = @i_dpi
		and		STATUS = 'A'

		IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se logro eliminar el registro.', 16, 1)
			RETURN 1
		END
	COMMIT TRAN
END
IF(@i_operation_type = 'GET_EMPLOYEE')
BEGIN
	IF NOT EXISTS(SELECT TOP 1 * FROM agex_employee WHERE EMPLOYEE_DPI = @i_dpi)
	BEGIN
		RAISERROR('El empleado a actualizar no existe.', 16, 1)
		RETURN 1
	END

	select	TOP 1
			e.EMPLOYEE_ID,
			e.EMPLOYEE_NAME,
			e.EMPLOYEE_DPI,
			ea.ADDRESS_LINE_1,
			ee.EMAIL
	from	agex_employee e,
			agex_employee_address ea,
			agex_employee_email ee
	where	e.EMPLOYEE_DPI = @i_dpi
	and		ea.EMPLOYEE_DPI = @i_dpi
	and		ee.EMPLOYEE_DPI = @i_dpi
	and		e.STATUS = 'A'

	IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
	BEGIN
		RAISERROR('No se encuentra el empleado a buscar.', 16, 1)
		RETURN 1
	END
END
IF(@i_operation_type = 'GET_EMPLOYEES')
BEGIN
	select	e.EMPLOYEE_ID,
			e.EMPLOYEE_NAME,
			e.EMPLOYEE_DPI,
			ea.ADDRESS_LINE_1,
			ee.EMAIL
	from	agex_employee e,
			agex_employee_address ea,
			agex_employee_email ee
	where	e.EMPLOYEE_DPI = @i_dpi
	and		ea.EMPLOYEE_DPI = @i_dpi
	and		ee.EMPLOYEE_DPI = @i_dpi
	and		e.STATUS = 'A'

	IF(@@ERROR > 0 OR @@ROWCOUNT = 0)
	BEGIN
		RAISERROR('No se encuentran empleados activos.', 16, 1)
		RETURN 1
	END
END


