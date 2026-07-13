-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 13, 2026 at 09:35 AM
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
-- Database: `isarms_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `bills`
--

CREATE TABLE `bills` (
  `bill_id` int(11) NOT NULL,
  `tenant_id` int(11) DEFAULT NULL,
  `unit_id` int(11) DEFAULT NULL,
  `billing_month` varchar(20) DEFAULT NULL,
  `base_rent` decimal(10,2) DEFAULT NULL,
  `addtional_charges` decimal(10,2) DEFAULT NULL,
  `due_date` date DEFAULT NULL,
  `status` enum('paid','partial','unpaid') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `bills`
--

INSERT INTO `bills` (`bill_id`, `tenant_id`, `unit_id`, `billing_month`, `base_rent`, `addtional_charges`, `due_date`, `status`) VALUES
(1, 1, 9, '2026-06', 35000.00, 0.00, '2026-06-15', 'paid'),
(2, 2, 10, '2026-07', 14000.00, 500.00, '2026-07-20', 'partial'),
(3, 3, 5, '2026-07', 18500.00, 0.00, '2026-07-18', 'unpaid'),
(4, 5, 2, '2026-07', 8800.00, 200.00, '2026-07-22', 'paid'),
(5, 6, 6, '2026-07', 13000.00, 0.00, '2026-07-15', 'partial'),
(6, 8, 3, '2026-06', 12500.00, 300.00, '2026-06-20', 'paid'),
(7, 10, 1, '2026-07', 8500.00, 0.00, '2026-07-25', 'unpaid'),
(8, 7, 7, '2026-05', 13500.00, 0.00, '2026-05-20', 'paid'),
(9, 4, 4, '2026-06', 18000.00, 0.00, '2026-06-25', 'paid');

-- --------------------------------------------------------

--
-- Table structure for table `expenses`
--

CREATE TABLE `expenses` (
  `expense_id` int(11) NOT NULL,
  `expense_type` varchar(50) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `amount` decimal(10,2) DEFAULT NULL,
  `recorded_by` varchar(100) DEFAULT NULL,
  `unit_id` int(11) DEFAULT NULL,
  `expense_date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `expenses`
--

INSERT INTO `expenses` (`expense_id`, `expense_type`, `description`, `amount`, `recorded_by`, `unit_id`, `expense_date`) VALUES
(1, 'Utilities', 'Common area electricity', 4500.00, 'admin', 2, '2026-06-15'),
(2, 'Maintenance', 'Elevator servicing', 7500.00, 'admin', 9, '2026-05-20'),
(3, 'Repair', 'Plumbing fix for unit 203', 2000.00, 'admin', 5, '2026-04-10'),
(4, 'Pest Control', 'Quarterly pest control service', 5200.00, 'admin', 7, '2026-03-05'),
(5, 'Repair', 'Window replacement unit 303', 1800.00, 'admin', 8, '2026-02-18'),
(6, 'Utilities', '', 2540.00, 'admin', 2, '2026-07-13'),
(7, 'Utilities', '', 4500.00, 'admin', 8, '2026-07-13'),
(8, 'Utilities', '', 2500.00, 'admin', 1, '2026-07-13'),
(9, 'Utilities', '', 3500.00, 'admin', 9, '2026-07-13'),
(10, 'Utilities', '', 1980.00, 'admin', 4, '2026-07-13');

-- --------------------------------------------------------

--
-- Table structure for table `leases`
--

CREATE TABLE `leases` (
  `lease_id` int(11) NOT NULL,
  `tenant_id` int(11) DEFAULT NULL,
  `unit_id` int(11) DEFAULT NULL,
  `lease_start` date DEFAULT NULL,
  `lease_end` date DEFAULT NULL,
  `monthly_rent` decimal(10,2) DEFAULT NULL,
  `security_deposit` decimal(10,2) DEFAULT NULL,
  `status` enum('active','terminated','expired') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `leases`
--

INSERT INTO `leases` (`lease_id`, `tenant_id`, `unit_id`, `lease_start`, `lease_end`, `monthly_rent`, `security_deposit`, `status`) VALUES
(1, 9, 1, '2025-03-01', '2025-09-30', 8500.00, 17000.00, 'expired'),
(2, 10, 2, '2026-01-05', '2026-02-04', 8800.00, 17600.00, 'terminated'),
(3, 8, 3, '2026-01-15', '2027-01-14', 12500.00, 25000.00, 'active'),
(4, 11, 4, '2026-01-20', '2026-02-19', 18000.00, 36000.00, 'terminated'),
(5, 4, 4, '2026-02-20', '2026-03-19', 18000.00, 36000.00, 'terminated'),
(6, 3, 5, '2026-02-01', '2026-02-28', 18500.00, 37000.00, 'terminated'),
(7, 6, 6, '2026-02-05', '2026-03-04', 13000.00, 26000.00, 'expired'),
(8, 7, 7, '2026-02-10', '2026-03-09', 13500.00, 27000.00, 'terminated'),
(9, 12, 8, '2026-02-14', '2026-03-13', 19000.00, 38000.00, 'expired'),
(10, 1, 9, '2026-02-01', '2026-02-28', 35000.00, 70000.00, 'terminated'),
(11, 2, 10, '2026-02-05', '2026-03-04', 14000.00, 28000.00, 'terminated'),
(12, 5, 2, '2026-02-10', '2026-03-09', 8800.00, 17600.00, 'terminated'),
(13, 9, 5, '2026-03-15', '2026-04-14', 18500.00, 37000.00, 'terminated'),
(14, 6, 6, '2026-03-05', '2026-04-04', 13000.00, 26000.00, 'terminated'),
(15, 7, 7, '2026-03-10', '2026-04-09', 13500.00, 27000.00, 'terminated'),
(16, 12, 8, '2026-03-14', '2026-04-13', 19000.00, 38000.00, 'terminated'),
(17, 1, 9, '2026-04-01', '2026-04-30', 35000.00, 70000.00, 'terminated'),
(18, 2, 10, '2026-04-05', '2026-05-04', 14000.00, 28000.00, 'terminated'),
(19, 5, 2, '2026-05-10', '2026-06-09', 8800.00, 17600.00, 'terminated'),
(20, 9, 5, '2026-05-15', '2026-06-14', 18500.00, 37000.00, 'terminated'),
(21, 6, 6, '2026-05-05', '2026-06-04', 13000.00, 26000.00, 'terminated'),
(22, 7, 7, '2026-05-10', '2026-06-09', 13500.00, 27000.00, 'terminated'),
(23, 12, 8, '2026-05-14', '2026-06-13', 19000.00, 38000.00, 'terminated'),
(24, 1, 9, '2026-06-01', '2026-06-30', 35000.00, 70000.00, 'terminated'),
(25, 2, 10, '2026-06-05', '2026-06-30', 14000.00, 28000.00, 'terminated'),
(26, 4, 4, '2026-06-10', '2026-06-30', 18000.00, 36000.00, 'terminated'),
(27, 10, 1, '2026-07-01', '2026-08-30', 8500.00, 17000.00, 'active'),
(28, 5, 2, '2026-07-10', '2026-08-09', 8800.00, 17600.00, 'active'),
(29, 3, 5, '2026-07-05', '2026-08-04', 18500.00, 37000.00, 'active'),
(30, 6, 6, '2026-07-08', '2026-08-07', 13000.00, 26000.00, 'active'),
(31, 2, 10, '2026-07-13', '2026-08-12', 14000.00, 28000.00, 'terminated');

-- --------------------------------------------------------

--
-- Table structure for table `payments`
--

CREATE TABLE `payments` (
  `payment_id` int(11) NOT NULL,
  `bill_id` int(11) DEFAULT NULL,
  `amount_paid` decimal(10,2) DEFAULT NULL,
  `payment_date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `payments`
--

INSERT INTO `payments` (`payment_id`, `bill_id`, `amount_paid`, `payment_date`) VALUES
(1, 1, 35000.00, '2026-06-14'),
(2, 2, 7000.00, '2026-07-19'),
(3, 4, 9000.00, '2026-07-21'),
(4, 5, 6000.00, '2026-07-14'),
(5, 6, 12800.00, '2026-06-19'),
(6, 8, 13500.00, '2026-05-19'),
(7, 9, 18000.00, '2026-06-24');

-- --------------------------------------------------------

--
-- Table structure for table `tenants`
--

CREATE TABLE `tenants` (
  `tenant_id` int(11) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `full_name` varchar(100) DEFAULT NULL,
  `contact_no` varchar(20) DEFAULT NULL,
  `emergency_contact` varchar(100) DEFAULT NULL,
  `gov_id` varchar(100) DEFAULT NULL,
  `account_status` enum('active','inactive') NOT NULL DEFAULT 'active'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tenants`
--

INSERT INTO `tenants` (`tenant_id`, `user_id`, `full_name`, `contact_no`, `emergency_contact`, `gov_id`, `account_status`) VALUES
(1, 2, 'Ramon Reyes', '09171234001', 'Elena Reyes - 09171239001', 'PSA-0001-2024', 'active'),
(2, 3, 'Ana Garcia', '09171234002', 'Luis Garcia - 09171239002', 'PSA-0002-2024', 'active'),
(3, 4, 'Paolo Bautista', '09171234003', 'Grace Bautista - 09171239003', 'PSA-0003-2024', 'active'),
(4, 5, 'Carmen Flores', '09171234004', 'Jose Flores - 09171239004', 'PSA-0004-2024', 'active'),
(5, 6, 'Stephaniejen Topinio', '09186959115', 'Marites Topinio - 09171239005', 'PSA-0005-2024', 'active'),
(6, 7, 'Juan Dela Cruz', '09171234006', 'Maria Dela Cruz - 09171239006', 'PSA-0006-2024', 'active'),
(7, 8, 'Maria Santos', '09171234007', 'Pedro Santos - 09171239007', 'PSA-0007-2024', 'active'),
(8, 9, 'Jose Rizal', '09171234008', 'Josefa Rizal - 09171239008', 'PSA-0008-2024', 'active'),
(9, 10, 'Grace Lim', '09171234009', 'Henry Lim - 09171239009', 'PSA-0009-2024', 'inactive'),
(10, 11, 'Mark Villanueva', '09171234010', 'Sarah Villanueva - 09171239010', 'PSA-0010-2024', 'active'),
(11, 12, 'Liza Cruz', '09171234011', 'Rico Cruz - 09171239011', 'PSA-0011-2024', 'inactive'),
(12, 13, 'Angelo Torres', '09171234012', 'Nina Torres - 09171239012', 'PSA-0012-2024', 'active');

-- --------------------------------------------------------

--
-- Table structure for table `units`
--

CREATE TABLE `units` (
  `unit_id` int(11) NOT NULL,
  `unit_number` varchar(10) DEFAULT NULL,
  `type` varchar(10) DEFAULT NULL,
  `floor` varchar(10) DEFAULT NULL,
  `monthly_rate` decimal(10,2) DEFAULT NULL,
  `unit_status` enum('occupied','available','maintenance') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `units`
--

INSERT INTO `units` (`unit_id`, `unit_number`, `type`, `floor`, `monthly_rate`, `unit_status`) VALUES
(1, '101', 'standard', '1', 8500.00, 'occupied'),
(2, '102', 'standard', '1', 8800.00, 'occupied'),
(3, '201', 'superior', '2', 12500.00, 'occupied'),
(4, '202', 'deluxe', '2', 18000.00, 'available'),
(5, '203', 'deluxe', '2', 18500.00, 'occupied'),
(6, '301', 'superior', '3', 13000.00, 'occupied'),
(7, '302', 'superior', '3', 13500.00, 'available'),
(8, '303', 'deluxe', '3', 19000.00, 'available'),
(9, '401', 'executive ', '4', 35000.00, 'available'),
(10, '402', 'superior', '4', 14000.00, 'available');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(45) NOT NULL,
  `role` enum('admin','tenant') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `username`, `password`, `role`) VALUES
(1, 'admin', 'Admin!Pass001', 'admin'),
(2, 'rreyes', 'ivan0123', 'tenant'),
(3, 'agarcia', 'Tenant!Pass002', 'tenant'),
(4, 'pbautista', 'Tenant!Pass003', 'tenant'),
(5, 'cflores', 'Tenant!Pass004', 'tenant'),
(6, 'stopinio', 'Tenant!Pass005', 'tenant'),
(7, 'jdelacruz', 'Tenant!Pass006', 'tenant'),
(8, 'msantos', 'Tenant!Pass007', 'tenant'),
(9, 'jrizal', 'Tenant!Pass008', 'tenant'),
(10, 'glim', 'Tenant!Pass009', 'tenant'),
(11, 'mvillanueva', 'Tenant!Pass010', 'tenant'),
(12, 'lcruz', 'Tenant!Pass011', 'tenant'),
(13, 'atorres', 'Tenant!Pass012', 'tenant');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `bills`
--
ALTER TABLE `bills`
  ADD PRIMARY KEY (`bill_id`),
  ADD KEY `tenant_id_idx` (`tenant_id`),
  ADD KEY `unit_id_idx` (`unit_id`);

--
-- Indexes for table `expenses`
--
ALTER TABLE `expenses`
  ADD PRIMARY KEY (`expense_id`),
  ADD UNIQUE KEY `expense_id_UNIQUE` (`expense_id`),
  ADD KEY `unit_id_idx` (`unit_id`);

--
-- Indexes for table `leases`
--
ALTER TABLE `leases`
  ADD PRIMARY KEY (`lease_id`),
  ADD UNIQUE KEY `lease_id_UNIQUE` (`lease_id`),
  ADD KEY `tenant_id_idx` (`tenant_id`),
  ADD KEY `unit_id_idx` (`unit_id`);

--
-- Indexes for table `payments`
--
ALTER TABLE `payments`
  ADD PRIMARY KEY (`payment_id`),
  ADD UNIQUE KEY `payment_id_UNIQUE` (`payment_id`),
  ADD KEY `bill_id_idx` (`bill_id`);

--
-- Indexes for table `tenants`
--
ALTER TABLE `tenants`
  ADD PRIMARY KEY (`tenant_id`),
  ADD UNIQUE KEY `tenant_id_UNIQUE` (`tenant_id`),
  ADD KEY `user_id_idx` (`user_id`);

--
-- Indexes for table `units`
--
ALTER TABLE `units`
  ADD PRIMARY KEY (`unit_id`),
  ADD UNIQUE KEY `unit_id_UNIQUE` (`unit_id`),
  ADD UNIQUE KEY `unit_number_UNIQUE` (`unit_number`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `user_id_UNIQUE` (`user_id`),
  ADD UNIQUE KEY `password_UNIQUE` (`password`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `bills`
--
ALTER TABLE `bills`
  MODIFY `bill_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `expenses`
--
ALTER TABLE `expenses`
  MODIFY `expense_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `leases`
--
ALTER TABLE `leases`
  MODIFY `lease_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT for table `payments`
--
ALTER TABLE `payments`
  MODIFY `payment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `tenants`
--
ALTER TABLE `tenants`
  MODIFY `tenant_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `units`
--
ALTER TABLE `units`
  MODIFY `unit_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `bills`
--
ALTER TABLE `bills`
  ADD CONSTRAINT `fk_bills_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`tenant_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_bills_unit_id` FOREIGN KEY (`unit_id`) REFERENCES `units` (`unit_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `expenses`
--
ALTER TABLE `expenses`
  ADD CONSTRAINT `fk_expenses_unit_id` FOREIGN KEY (`unit_id`) REFERENCES `units` (`unit_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `leases`
--
ALTER TABLE `leases`
  ADD CONSTRAINT `fk_leases_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`tenant_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_leases_unit_id` FOREIGN KEY (`unit_id`) REFERENCES `units` (`unit_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `payments`
--
ALTER TABLE `payments`
  ADD CONSTRAINT `fk_payments_bill_id` FOREIGN KEY (`bill_id`) REFERENCES `bills` (`bill_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tenants`
--
ALTER TABLE `tenants`
  ADD CONSTRAINT `fk_tenants_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
