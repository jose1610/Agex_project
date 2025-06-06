

CREATE PROCEDURE sp_activity

@i_operation_type	VARCHAR(50) = NULL,
@i_activity_id		VARCHAR(32) = NULL,
@i_json_request		VARCHAR(MAX) = NULL,
@i_json_response	VARCHAR(MAX) = NULL,
@i_type				VARCHAR(50) = NULL,
@i_status_code		VARCHAR(3) = NULL

AS

IF(@i_operation_type = 'INSERT_ACTIVITY')
BEGIN
	BEGIN TRAN
		INSERT INTO agex_activity
		(
				ACTIVITY_ID,		JSON_REQUEST,		TYPE,			STATUSCODE, 
				CREATE_DATETIME
		)
		values	(
				@i_activity_id,		@i_json_request,	@i_type,		@i_status_code,
				GETDATE()
				)
		IF (@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se ha logrado insertar el registro (activity).', 16, 1)
			RETURN 1
		END
	COMMIT TRAN

END
IF(@i_operation_type = 'UPDATE_ACTIVITY')
BEGIN
	IF NOT EXISTS(SELECT TOP 1 * FROM agex_activity WHERE ACTIVITY_ID = @i_activity_id)
	BEGIN
		RAISERROR('No se encuentra el ACTIVITY_ID buscado.', 16, 1)
		RETURN 1
	END

	BEGIN TRAN
		update	agex_activity
		set		JSON_RESPONSE = @i_json_response,
				UPDATE_DATETIME = GETDATE()
		where	ACTIVITY_ID = @i_activity_id

		IF (@@ERROR > 0 OR @@ROWCOUNT = 0)
		BEGIN
			ROLLBACK
			RAISERROR('No se ha logrado insertar el registro (activity).', 16, 1)
			RETURN 1
		END
	COMMIT TRAN
END


