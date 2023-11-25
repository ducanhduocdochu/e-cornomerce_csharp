DELIMITER //

CREATE PROCEDURE CreateKeyToken(
    IN p_user_id VARCHAR(255),
    IN p_publish_key VARCHAR(1024),
    IN p_private_key VARCHAR(2048),
    IN p_refresh_token VARCHAR(1024)
)
BEGIN
    DECLARE exit handler for sqlexception
    BEGIN
        ROLLBACK;
    END;
    
    START TRANSACTION;
    INSERT INTO key_tokens (user_id, public_key, private_key, refresh_token)
    VALUES (p_user_id, p_publish_key, p_private_key, p_refresh_token);	    
    SELECT user_id, public_key, private_key, refresh_token FROM key_tokens WHERE user_id = p_user_id;
    
    COMMIT;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE UpdateKeyToken(
    IN p_user_id VARCHAR(255),
    IN p_publish_key VARCHAR(1024),
    IN p_private_key VARCHAR(2048),
    IN p_refresh_token VARCHAR(1024)
)
BEGIN
    DECLARE exit handler for sqlexception
    BEGIN
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    UPDATE key_tokens
    SET 
        public_key = p_publish_key,
        private_key = p_private_key,
        refresh_token = p_refresh_token
    WHERE user_id = p_user_id;
    
    SELECT user_id, public_key, private_key, refresh_token
    FROM key_tokens
    WHERE user_id = p_user_id;
    
    COMMIT;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE DeleteKeyToken(
    IN p_user_id VARCHAR(255)
)
BEGIN
    DECLARE exit handler for sqlexception
    BEGIN
        ROLLBACK;
    END;
    
    START TRANSACTION;
    DELETE FROM key_tokens WHERE user_id = p_user_id;
    SELECT p_user_id as user_id, "aaaaaaaaaaaaaaaaaaaaa" as public_key, "aaaaaaaaaaaaaaaaaaaaa" as private_key, "aaaaaaaaaaaaaaaaaaaaa" as refresh_token;
    
    COMMIT;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE AddTokenUsed(
    IN p_user_id VARCHAR(255),
    IN p_refresh_token VARCHAR(1024)
)
BEGIN
    DECLARE exit handler for sqlexception
    BEGIN
        ROLLBACK;
    END;
    
    START TRANSACTION;
    INSERT INTO refresh_token_useds (user_id, refresh_token)
    VALUES (p_user_id, p_refresh_token);	    
    SELECT id, user_id, refresh_token FROM refresh_token_useds WHERE user_id = p_user_id;
    
    COMMIT;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE CreateUser(
    IN p_user_id VARCHAR(255),
    IN p_username VARCHAR(255),
    IN p_email VARCHAR(255),
    IN p_password VARCHAR(255),
    IN p_status BOOLEAN,
    IN p_verify BOOLEAN,
    IN p_is_user BOOLEAN,
    IN p_is_shop BOOLEAN,
    IN p_is_admin BOOLEAN
)
BEGIN
    DECLARE exit handler for sqlexception
    BEGIN
        ROLLBACK;
    END;
    START TRANSACTION;
        INSERT INTO users (user_id, username, email, password, status, verify, is_user, is_shop, is_admin)
        VALUES (p_user_id, p_username, p_email, p_password, p_status, p_verify, p_is_user, p_is_shop, p_is_admin);
        SELECT user_id, username, email, password, status, verify, is_user, is_shop, is_admin FROM users WHERE user_id = p_user_id;
    COMMIT;
END //

DELIMITER ;	


