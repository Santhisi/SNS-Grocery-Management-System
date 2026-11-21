-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: May 25, 2026 at 10:31 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `grocery_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `bills`
--

CREATE TABLE `bills` (
  `bill_id` int(11) NOT NULL,
  `bill_date` datetime NOT NULL DEFAULT current_timestamp(),
  `total_amount` decimal(10,2) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `bills`
--

INSERT INTO `bills` (`bill_id`, `bill_date`, `total_amount`) VALUES
(1, '2025-11-23 14:25:51', 1200.00),
(2, '2025-11-23 14:25:51', 850.00),
(3, '2025-11-23 14:25:51', 2300.00),
(4, '2025-11-26 15:06:42', 180.00),
(5, '2025-11-26 18:27:33', 2880.00),
(6, '2025-11-26 19:31:25', 280.00),
(7, '2025-11-26 19:43:27', 200.00),
(8, '2025-11-26 19:47:20', 1040.00),
(9, '2025-11-26 19:59:38', 870.00),
(10, '2025-11-26 22:56:38', 1060.00),
(11, '2025-11-28 01:52:06', 600.00),
(12, '2025-11-28 11:01:04', 480.00),
(13, '2025-12-01 15:11:51', 660.00),
(14, '2025-12-01 16:26:27', 2460.00),
(15, '2026-03-08 14:01:34', 1250.00),
(16, '2026-03-23 16:01:30', 1450.00),
(17, '2026-03-26 08:47:22', 530.00),
(18, '2026-05-11 09:07:11', 11950.00),
(19, '2026-05-11 09:07:32', 1070.00);

-- --------------------------------------------------------

--
-- Table structure for table `bill_items`
--

CREATE TABLE `bill_items` (
  `bill_item_id` int(11) NOT NULL,
  `bill_id` int(11) NOT NULL,
  `item_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `price` decimal(10,2) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `bill_items`
--

INSERT INTO `bill_items` (`bill_item_id`, `bill_id`, `item_id`, `quantity`, `price`) VALUES
(1, 1, 1, 2, 350.00),
(2, 1, 3, 1, 250.00),
(3, 2, 4, 2, 180.00),
(4, 3, 7, 1, 750.00),
(5, 3, 9, 3, 220.00),
(6, 4, 4, 1, 180.00),
(7, 5, 2, 6, 330.00),
(8, 5, 6, 2, 450.00),
(9, 6, 8, 1, 280.00),
(10, 7, 10, 1, 200.00),
(11, 8, 14, 8, 130.00),
(12, 9, 1, 1, 350.00),
(13, 9, 14, 4, 130.00),
(14, 10, 1, 1, 350.00),
(15, 10, 14, 1, 130.00),
(16, 10, 2, 1, 330.00),
(17, 10, 3, 1, 250.00),
(18, 11, 15, 5, 120.00),
(19, 12, 1, 1, 350.00),
(20, 12, 14, 1, 130.00),
(21, 13, 2, 2, 330.00),
(22, 14, 17, 2, 800.00),
(23, 14, 15, 2, 100.00),
(24, 14, 2, 2, 330.00),
(25, 15, 56, 1, 1250.00),
(26, 16, 4, 1, 180.00),
(27, 16, 17, 1, 790.00),
(28, 16, 22, 1, 480.00),
(29, 17, 15, 1, 100.00),
(30, 17, 4, 1, 180.00),
(31, 17, 3, 1, 250.00),
(32, 18, 15, 1, 100.00),
(33, 18, 17, 15, 790.00),
(34, 19, 15, 1, 100.00),
(35, 19, 4, 1, 180.00),
(36, 19, 17, 1, 790.00);

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `category_id` int(11) NOT NULL,
  `category_name` varchar(100) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`category_id`, `category_name`) VALUES
(1, 'Beverages'),
(2, 'Snacks'),
(3, 'Stationery'),
(14, 'Household Supplies'),
(11, 'Bakery'),
(13, 'Dairy Products'),
(15, 'Personal care'),
(16, 'Baby & Kids'),
(17, 'Pet Supplies'),
(18, 'Spices');

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `item_id` int(11) NOT NULL,
  `item_name` varchar(100) NOT NULL,
  `category_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `price` decimal(10,2) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`item_id`, `item_name`, `category_id`, `quantity`, `price`) VALUES
(2, 'Pepsi 1L', 1, 29, 330.00),
(3, 'Potato Chips Lays', 2, 98, 250.00),
(4, 'Chocolate Bar - KitKat', 2, 76, 180.00),
(15, 'Donuts', 11, 13, 100.00),
(23, 'Highland Yogurt', 13, 50, 70.00),
(22, 'Butter (480g)', 13, 19, 480.00),
(21, 'Wipes', 16, 30, 570.00),
(17, 'Cheese', 13, 31, 790.00),
(20, 'Diapers', 16, 26, 890.00),
(19, 'Fresh Milk 1L', 13, 15, 550.00),
(24, 'Eggs', 13, 60, 35.00),
(25, 'Bread', 11, 15, 200.00),
(26, 'Tea bun', 11, 20, 80.00),
(28, 'Chocolate Biscuit (100g)', 2, 50, 130.00),
(29, 'Mentos', 2, 60, 50.00),
(30, 'Red Bull', 1, 30, 180.00),
(31, 'Iced coffee 1L', 1, 54, 550.00),
(32, 'Detergent (180ml)', 14, 26, 300.00),
(33, 'Dish soap', 14, 45, 140.00),
(34, 'Tissue Paper', 14, 40, 280.00),
(35, 'Garbage bags (1pac)', 14, 40, 400.00),
(36, 'Cleaning sprays', 14, 25, 620.00),
(37, 'Dandex Shampoo (490ml)', 15, 34, 690.00),
(38, 'Sunsilk Shampoo (450ml)', 15, 32, 480.00),
(39, 'Signal Toothpaste', 15, 23, 350.00),
(40, 'Clogaurd Toothpaste', 15, 26, 350.00),
(41, 'Razor blades', 15, 61, 140.00),
(42, 'Lux soap', 15, 23, 190.00),
(43, 'Baby soap', 15, 42, 165.00),
(44, 'Lux Body lotion (400ml)', 15, 20, 580.00),
(45, 'Cat food', 17, 14, 960.00),
(46, 'Dog food', 17, 18, 1080.00),
(47, 'Salt (500g)', 18, 20, 400.00),
(48, 'Sugar (500g)', 18, 45, 650.00),
(49, 'Pepper (200g)', 18, 36, 280.00),
(50, 'Turmeric (200g)', 18, 25, 280.00),
(51, 'Chili Powder (200g)', 18, 42, 280.00),
(52, 'Cocacola 1L', 1, 74, 330.00),
(53, 'Jelly Yougurt', 13, 41, 85.00),
(54, 'Waffes (200g)', 2, 65, 230.00),
(55, 'Cream Craker (190g)', 2, 60, 200.00),
(56, 'Sugar 1KG', 18, 44, 1250.00),
(57, 'Salt (250g)', 18, 20, 200.00);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(100) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `username`, `password`) VALUES
(1, 'admin', 'admin123'),
(2, 'cashier1', 'cashier123'),
(3, 'a', 'a');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `bills`
--
ALTER TABLE `bills`
  ADD PRIMARY KEY (`bill_id`);

--
-- Indexes for table `bill_items`
--
ALTER TABLE `bill_items`
  ADD PRIMARY KEY (`bill_item_id`);

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`category_id`);

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`item_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `bills`
--
ALTER TABLE `bills`
  MODIFY `bill_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `bill_items`
--
ALTER TABLE `bill_items`
  MODIFY `bill_item_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `category_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `item_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=58;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
