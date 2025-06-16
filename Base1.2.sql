SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema hotel_system
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `hotel_system` ;

-- -----------------------------------------------------
-- Schema hotel_system
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `hotel_system` DEFAULT CHARACTER SET utf8 ;
SHOW WARNINGS;
USE `hotel_system` ;

-- -----------------------------------------------------
-- Table `hotel_system`.`Guests`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `hotel_system`.`Guests` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `hotel_system`.`Guests` (
  `idGuests` INT NOT NULL,
  `Lastname` VARCHAR(45) NOT NULL,
  `Firstname` VARCHAR(45) NOT NULL,
  `Othername` VARCHAR(45) NOT NULL,
  `Contact_num` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idGuests`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `hotel_system`.`Room_stock`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `hotel_system`.`Room_stock` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `hotel_system`.`Room_stock` (
  `idRoom` INT NOT NULL,
  `Floor` VARCHAR(45) NOT NULL,
  `Number` INT NOT NULL,
  `Category` VARCHAR(200) NOT NULL,
  PRIMARY KEY (`idRoom`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `hotel_system`.`Guests_currently_living_in_the_hotel`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `hotel_system`.`Guests_currently_living_in_the_hotel` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `hotel_system`.`Guests_currently_living_in_the_hotel` (
  `Number` INT NOT NULL,
  `Category` VARCHAR(200) NOT NULL,
  `Guests` VARCHAR(200) NOT NULL,
  `Entry` DATE NOT NULL,
  `Exit` DATE NULL,
  `Guests_idGuests` INT NOT NULL,
  `Room_stock_idRoom` INT NOT NULL,
  PRIMARY KEY (`Number`),
  CONSTRAINT `fk_Guests_currently_living_in_the_hotel_Guests`
    FOREIGN KEY (`Guests_idGuests`)
    REFERENCES `hotel_system`.`Guests` (`idGuests`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Guests_currently_living_in_the_hotel_Room_stock1`
    FOREIGN KEY (`Room_stock_idRoom`)
    REFERENCES `hotel_system`.`Room_stock` (`idRoom`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;
CREATE INDEX `fk_Guests_currently_living_in_the_hotel_Guests_idx` ON `hotel_system`.`Guests_currently_living_in_the_hotel` (`Guests_idGuests` ASC) VISIBLE;

SHOW WARNINGS;
CREATE INDEX `fk_Guests_currently_living_in_the_hotel_Room_stock1_idx` ON `hotel_system`.`Guests_currently_living_in_the_hotel` (`Room_stock_idRoom` ASC) VISIBLE;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `hotel_system`.`Report`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `hotel_system`.`Report` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `hotel_system`.`Report` (
  `Number` INT NOT NULL,
  `Category` VARCHAR(200) NOT NULL,
  `Status` VARCHAR(45) NOT NULL,
  `Date_exit` DATE NULL,
  `Room_stock_idRoom` INT NOT NULL,
  PRIMARY KEY (`Number`),
  CONSTRAINT `fk_Report_Room_stock1`
    FOREIGN KEY (`Room_stock_idRoom`)
    REFERENCES `hotel_system`.`Room_stock` (`idRoom`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;
CREATE INDEX `fk_Report_Room_stock1_idx` ON `hotel_system`.`Report` (`Room_stock_idRoom` ASC) VISIBLE;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `hotel_system`.`users`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `hotel_system`.`users` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `hotel_system`.`users` (
  `user_id` INT NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(45) NOT NULL,
  `pass_us` VARCHAR(45) NOT NULL,
  `role` VARCHAR(45) NOT NULL,
  `locked_us` TINYINT NOT NULL,
  `attem_us` INT NOT NULL,
  `last_us` DATETIME NULL,
  `first_us` TINYINT NULL,
  PRIMARY KEY (`user_id`))
ENGINE = InnoDB;

SHOW WARNINGS;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;