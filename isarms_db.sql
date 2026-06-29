-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 29, 2026 at 06:39 AM
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
(1, 1, 1, '2025-01', 8500.00, 500.00, '2025-01-10', 'paid'),
(2, 1, 1, '2025-02', 8500.00, 500.00, '2025-02-10', 'paid'),
(3, 2, 2, '2025-01', 8000.00, 450.00, '2025-01-10', 'paid'),
(4, 2, 2, '2025-02', 8000.00, 450.00, '2025-02-10', 'unpaid'),
(5, 3, 4, '2025-01', 12500.00, 600.00, '2025-01-15', 'paid'),
(6, 3, 4, '2025-02', 12500.00, 600.00, '2025-02-15', 'partial'),
(7, 4, 5, '2025-01', 18000.00, 750.00, '2025-01-05', 'paid'),
(8, 4, 5, '2025-02', 18000.00, 750.00, '2025-02-05', 'unpaid'),
(9, 6, 8, '2025-01', 13000.00, 500.00, '2025-01-20', 'paid'),
(10, 7, 10, '2025-01', 35000.00, 1200.00, '2025-01-20', 'partial'),
(11, 9, 9, '2025-01', 19000.00, 600.00, '2025-01-25', 'unpaid'),
(12, 9, 9, '2025-02', 19000.00, 600.00, '2025-02-25', 'unpaid');

-- --------------------------------------------------------

--
-- Table structure for table `expenses`
--

CREATE TABLE `expenses` (
  `expense_id` int(11) NOT NULL,
  `expense_type` varchar(50) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `amount` decimal(10,2) DEFAULT NULL,
  `unit_id` int(11) DEFAULT NULL,
  `expense_date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `expenses`
--

INSERT INTO `expenses` (`expense_id`, `expense_type`, `description`, `amount`, `unit_id`, `expense_date`) VALUES
(1, 'Maintenance', 'Aircon cleaning and repair', 2500.00, 6, '2025-01-12'),
(2, 'Utilities', 'Common area electricity bill', 4200.00, NULL, '2025-01-15'),
(3, 'Repair', 'Plumbing fix for unit 203', 1800.00, 6, '2025-01-20'),
(4, 'Maintenance', 'Painting touch-up unit 303', 3200.00, 9, '2025-02-02'),
(5, 'Pest Control', 'Quarterly pest control service', 5000.00, NULL, '2025-02-10'),
(6, 'Repair', 'Replace broken window unit 101', 1500.00, 1, '2025-02-18'),
(7, 'Maintenance', 'Elevator servicing', 7000.00, NULL, '2025-02-20'),
(8, 'Repair', 'Door lock replacement unit 401', 950.00, 10, '2025-03-01');

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
(1, 1, 1, '2024-01-01', '2024-12-31', 8000.00, 16000.00, 'expired'),
(2, 1, 1, '2025-01-01', '2025-12-31', 8500.00, 17000.00, 'active'),
(3, 2, 2, '2024-06-01', '2025-05-31', 8000.00, 16000.00, 'active'),
(4, 3, 4, '2024-03-01', '2025-02-28', 12500.00, 25000.00, 'active'),
(5, 4, 5, '2024-02-01', '2025-01-31', 18000.00, 36000.00, 'active'),
(6, 5, 7, '2023-11-01', '2024-10-31', 8200.00, 16400.00, 'terminated'),
(7, 6, 8, '2024-05-01', '2025-04-30', 13000.00, 26000.00, 'active'),
(8, 7, 10, '2024-01-15', '2025-01-14', 35000.00, 70000.00, 'active'),
(9, 8, 6, '2023-08-01', '2024-07-31', 17500.00, 35000.00, 'expired'),
(10, 9, 9, '2024-04-01', '2025-03-31', 19000.00, 38000.00, 'active');

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
(1, 1, 9000.00, '2025-01-09'),
(2, 2, 9000.00, '2025-02-09'),
(3, 3, 8450.00, '2025-01-08'),
(4, 5, 13100.00, '2025-01-14'),
(5, 6, 6000.00, '2025-02-16'),
(6, 7, 18750.00, '2025-01-04'),
(7, 9, 13500.00, '2025-01-19'),
(8, 10, 18000.00, '2025-01-21');

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
  `gov_id` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tenants`
--

INSERT INTO `tenants` (`tenant_id`, `user_id`, `full_name`, `contact_no`, `emergency_contact`, `gov_id`) VALUES
(1, 2, 'Juan Dela Cruz', '09171234001', 'Maria Dela Cruz - 09171239001', 'PSA-0001-2024'),
(2, 3, 'Maria Santos', '09171234002', 'Pedro Santos - 09171239002', 'PSA-0002-2024'),
(3, 4, 'Ramon Reyes', '09171234003', 'Elena Reyes - 09171239003', 'PSA-0003-2024'),
(4, 5, 'Ana Garcia', '09171234004', 'Luis Garcia - 09171239004', 'PSA-0004-2024'),
(5, 6, 'Liza Cruz', '09171234005', 'Mark Cruz - 09171239005', 'PSA-0005-2024'),
(6, 7, 'Paolo Bautista', '09171234006', 'Grace Bautista - 09171239006', 'PSA-0006-2024'),
(7, 8, 'Carmen Flores', '09171234007', 'Jose Flores - 09171239007', 'PSA-0007-2024'),
(8, 9, 'Miguel Torres', '09171234008', 'Rosa Torres - 09171239008', 'PSA-0008-2024'),
(9, 10, 'Jenny Ramos', '09171234009', 'Carlos Ramos - 09171239009', 'PSA-0009-2024');

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
(1, '101', 'Studio', '1', 8000.00, 'occupied'),
(2, '102', 'Studio', '1', 8000.00, 'occupied'),
(3, '103', '1BR', '1', 12000.00, 'available'),
(4, '201', '1BR', '2', 12500.00, 'occupied'),
(5, '202', '2BR', '2', 18000.00, 'occupied'),
(6, '203', '2BR', '2', 18000.00, 'maintenance'),
(7, '301', 'Studio', '3', 8500.00, 'occupied'),
(8, '302', '1BR', '3', 13000.00, 'occupied'),
(9, '303', '2BR', '3', 19000.00, 'available'),
(10, '401', 'Penthouse', '4', 35000.00, 'occupied');

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
(1, 'admin1', 'AdminPass123!', 'admin'),
(2, 'jdelacruz', 'Tenant!Pass001', 'tenant'),
(3, 'msantos', 'Tenant!Pass002', 'tenant'),
(4, 'rreyes', 'Tenant!Pass003', 'tenant'),
(5, 'agarcia', 'Tenant!Pass004', 'tenant'),
(6, 'lcruz', 'Tenant!Pass005', 'tenant'),
(7, 'pbautista', 'Tenant!Pass006', 'tenant'),
(8, 'cflores', 'Tenant!Pass007', 'tenant'),
(9, 'mtorres', 'Tenant!Pass008', 'tenant'),
(10, 'jramos', 'Tenant!Pass009', 'tenant');

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
  MODIFY `bill_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `expenses`
--
ALTER TABLE `expenses`
  MODIFY `expense_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `leases`
--
ALTER TABLE `leases`
  MODIFY `lease_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `payments`
--
ALTER TABLE `payments`
  MODIFY `payment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `tenants`
--
ALTER TABLE `tenants`
  MODIFY `tenant_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `units`
--
ALTER TABLE `units`
  MODIFY `unit_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

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
