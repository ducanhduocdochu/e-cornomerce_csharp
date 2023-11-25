CREATE TABLE users (
	user_id VARCHAR(255) NOT NULL PRIMARY KEY,
    username VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    status BOOLEAN NOT NULL, -- Replace with actual status values
    verify BOOLEAN NOT NULL,
    is_user BOOLEAN NOT NULL,
    is_shop BOOLEAN NOT NULL,
    is_admin BOOLEAN NOT NULL
);

CREATE TABLE user_info (
    user_id VARCHAR(255) PRIMARY KEY NOT NULL,
    address VARCHAR(255) NOT NULL,
    image VARCHAR(255) NOT NULL,
    delivery_address VARCHAR(255) NOT NULL,
    phone VARCHAR(255) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

CREATE TABLE key_tokens (
    user_id VARCHAR(255) PRIMARY KEY NOT NULL,
    public_key VARCHAR(1024) NOT NULL,
    private_key VARCHAR(2048) NOT NULL,
    refresh_token VARCHAR(1024) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

CREATE TABLE refresh_token_useds (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_id VARCHAR(255) NOT NULL,
    refresh_token VARCHAR(1024) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);