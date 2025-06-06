

CREATE PROCEDURE sp_user

@i_operation_type	VARCHAR(100) = NULL,
@i_user_id			VARCHAR(100) = NULL,
@i_user_password	VARCHAR(100) = NULL

AS

DECLARE	@w_attempts	int = 0

IF(@i_operation_type = 'REGISTER_USER')
BEGIN
	IF EXISTS(SELECT TOP 1 * FROM agex_user WHERE [USER_ID] = @i_user_id)
	BEGIN
		RAISERROR('Usuario a registrar ya existe.', 16, 1)
		RETURN 1
	END

	BEGIN TRAN
		INSERT INTO agex_user(USER_ID, USER_PASSWORD, CREATE_DATETIME, STATUS)
				values	(@i_user_id, @i_user_password, GETDATE(), 'A')

		IF (@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('Error a la hora de registrar usuario.', 16, 1)
			RETURN 1
		END
	COMMIT TRAN
END
IF(@i_operation_type = 'UPDATE_USER_PASSWORD')
BEGIN
	IF NOT EXISTS(SELECT TOP 1 * FROM agex_user WHERE [USER_ID] = @i_user_id)
	BEGIN
		RAISERROR('Usuario a editar no existe.', 16, 1)
		RETURN 1
	END

	BEGIN TRAN
		update	agex_user
		set		USER_PASSWORD = @i_user_password,
				UPDATE_DATETIME = GETDATE()
		where	USER_ID = @i_user_id

		IF (@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('Error a la hora de actualizar usuario.', 16, 1)
			RETURN 1
		END
	COMMIT TRAN
END
IF(@i_operation_type = 'UPDATE_USER_LAST_LOGIN')
BEGIN
	IF NOT EXISTS(SELECT TOP 1 * FROM agex_user WHERE [USER_ID] = @i_user_id)
	BEGIN
		RAISERROR('Usuario a editar no existe.', 16, 1)
		RETURN 1
	END

	BEGIN TRAN
		update	agex_user
		set		LAST_LOGIN = GETDATE()
		where	USER_ID = @i_user_id

		IF (@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('Error a la hora de actualizar usuario.', 16, 1)
			RETURN 1
		END
	COMMIT TRAN
END
IF(@i_operation_type = 'GET_USER_PASSWORD')
BEGIN
	IF NOT EXISTS(SELECT TOP 1 * FROM agex_user WHERE [USER_ID] = @i_user_id)
	BEGIN
		RAISERROR('Usuario ingresado no existe.', 16, 1)
		RETURN 1
	END

	select	USER_ID,
			USER_PASSWORD,
			ATTEMPTS
	from	agex_user
	where	USER_ID = @i_user_id

	IF (@@ERROR > 0 OR @@ROWCOUNT = 0)
	BEGIN
		ROLLBACK
		RAISERROR('Error a la hora de realizar la busqueda de usuario.', 16, 1)
		RETURN 1
	END
END
IF(@i_operation_type = 'UPDATE_USER_ATTEMPTS')
BEGIN
	IF NOT EXISTS(SELECT TOP 1 * FROM agex_user WHERE [USER_ID] = @i_user_id)
	BEGIN
		RAISERROR('Usuario ingresado no existe.', 16, 1)
		RETURN 1
	END

	select @w_attempts = ATTEMPTS
	from agex_user
	where	USER_ID = @i_user_id

	BEGIN TRAN
		update	agex_user
		set		ATTEMPTS = @w_attempts + 1
		where	USER_ID = @i_user_id

		IF (@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('Error a la hora de actualizar intentos de solicitud de ingreso.', 16, 1)
			RETURN 1
		END
	COMMIT TRAN
END
IF(@i_operation_type = 'GET_USER')
BEGIN
	IF NOT EXISTS(SELECT TOP 1 * FROM agex_user WHERE [USER_ID] = @i_user_id)
	BEGIN
		RAISERROR('Usuario ingresado no existe.', 16, 1)
		RETURN 1
	END

	select	USER_ID,
			USER_PASSWORD,
			ATTEMPTS,
			CREATE_DATETIME
	from	agex_user
	where	USER_ID = @i_user_id
	AND		STATUS = 'A'

	IF (@@ERROR > 0 OR @@ROWCOUNT = 0)
	BEGIN
		RAISERROR('Error en busqueda de usuario.', 16, 1)
		RETURN 1
	END
END
IF(@i_operation_type = 'GET_USERS')
BEGIN
	select	USER_ID,
			USER_PASSWORD,
			ATTEMPTS,
			CREATE_DATETIME
	from	agex_user
	WHERE	STATUS = 'A'

	IF (@@ERROR > 0 OR @@ROWCOUNT = 0)
	BEGIN
		RAISERROR('Error en busqueda de usuario.', 16, 1)
		RETURN 1
	END
END
