CREATE TABLE `items` (
	`id` bigint(64) NOT NULL AUTO_INCREMENT,
	`name` char(32) NOT NULL,
	`category_id` int(32) NOT NULL,
	`brand_id` int(32) NOT NULL,
	`unit_id` int(32) NOT NULL,
	`scale` FLOAT(64) NOT NULL DEFAULT '0',
	`price` FLOAT(64) NOT NULL DEFAULT '0',
	`quantity` int(64) NOT NULL DEFAULT '0',
	PRIMARY KEY (`id`)
);

CREATE TABLE `user_role` (
	`id` int(8) NOT NULL AUTO_INCREMENT,
	`name` char(16) NOT NULL,
	`restriction` BINARY(8) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE `users` (
	`id` int(16) NOT NULL AUTO_INCREMENT UNIQUE,
	`name` char(16) NOT NULL,
	`password` char(16) NOT NULL,
	`user_role` int(8) NOT NULL DEFAULT '0',
	PRIMARY KEY (`id`)
);

CREATE TABLE `transaction_type` (
	`id` int(8) NOT NULL AUTO_INCREMENT,
	`name` char(16) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE `transactions` (
	`id` bigint NOT NULL AUTO_INCREMENT,
	`user_id` int(16) NOT NULL,
	`timestamp` DATETIME NOT NULL,
	`type_id` DATETIME NOT NULL,
	`item_id` bigint(64) NOT NULL,
	`quantity` int(64) NOT NULL DEFAULT '0',
	PRIMARY KEY (`id`)
);

CREATE TABLE `unit_scale` (
	`id` int(8) NOT NULL AUTO_INCREMENT,
	`name` char(16) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE `brands` (
	`id` int(32) NOT NULL AUTO_INCREMENT,
	`name` char(32) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE `category` (
	`id` int(32) NOT NULL AUTO_INCREMENT,
	`name` char(32) NOT NULL,
	PRIMARY KEY (`id`)
);

ALTER TABLE `items` ADD CONSTRAINT `items_fk0` FOREIGN KEY (`category_id`) REFERENCES `category`(`id`);

ALTER TABLE `items` ADD CONSTRAINT `items_fk1` FOREIGN KEY (`brand_id`) REFERENCES `brands`(`id`);

ALTER TABLE `items` ADD CONSTRAINT `items_fk2` FOREIGN KEY (`unit_id`) REFERENCES `unit_scale`(`id`);

ALTER TABLE `users` ADD CONSTRAINT `users_fk0` FOREIGN KEY (`user_role`) REFERENCES `user_role`(`id`);

ALTER TABLE `transactions` ADD CONSTRAINT `transactions_fk0` FOREIGN KEY (`user_id`) REFERENCES `users`(`id`);

ALTER TABLE `transactions` ADD CONSTRAINT `transactions_fk1` FOREIGN KEY (`type_id`) REFERENCES `transaction_type`(`id`);

ALTER TABLE `transactions` ADD CONSTRAINT `transactions_fk2` FOREIGN KEY (`item_id`) REFERENCES `items`(`id`);

