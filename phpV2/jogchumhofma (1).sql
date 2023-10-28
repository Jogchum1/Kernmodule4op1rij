-- phpMyAdmin SQL Dump
-- version 4.9.4
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Gegenereerd op: 28 okt 2023 om 11:43
-- Serverversie: 10.6.12-MariaDB
-- PHP-versie: 7.4.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `jogchumhofma`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `Games`
--

CREATE TABLE `Games` (
  `id` int(11) NOT NULL,
  `Game` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Gegevens worden geëxporteerd voor tabel `Games`
--

INSERT INTO `Games` (`id`, `Game`) VALUES
(1, 'MiniBall'),
(2, 'MaxiBall'),
(3, 'MiniBlox'),
(4, 'MaxiBlox'),
(5, 'BallBlox');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `LastPlayedGames`
--

CREATE TABLE `LastPlayedGames` (
  `id` int(11) NOT NULL,
  `user_name` varchar(40) NOT NULL,
  `score` int(11) NOT NULL,
  `date` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `Score`
--

CREATE TABLE `Score` (
  `id` int(11) NOT NULL,
  `score` int(11) NOT NULL,
  `game_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `server_id` int(11) NOT NULL,
  `datetime` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Gegevens worden geëxporteerd voor tabel `Score`
--

INSERT INTO `Score` (`id`, `score`, `game_id`, `user_id`, `server_id`, `datetime`) VALUES
(1, 184184, 1, 1, 0, '2022-05-09 11:02:36'),
(2, 15225, 1, 2, 0, '2022-05-09 11:02:36'),
(3, 2314, 3, 1, 0, '2022-05-09 11:02:36'),
(4, 55136, 4, 3, 0, '2022-05-09 11:02:36'),
(5, 63742, 2, 3, 0, '2022-05-09 11:02:36'),
(6, 1, 3, 10, 0, '2022-05-09 11:02:55'),
(7, 1, 3, 10, 0, '2022-05-09 11:05:44'),
(8, 1, 3, 100, 0, '2022-05-09 11:06:35'),
(9, 1, 3, 100, 0, '2022-05-09 11:08:32'),
(10, 3, 3, 4, 4, '2022-05-23 10:44:59'),
(11, 3, 3, 4, 4, '2022-05-23 10:48:51'),
(12, 3, 3, 4, 4, '2022-05-23 10:55:38'),
(13, 3, 3, 4, 4, '2022-05-23 11:09:35'),
(25, 3, 3, 4, 1, '2022-05-23 11:14:19'),
(26, 3, 3, 4, 1, '2022-05-23 11:24:51'),
(27, 1, 3, 1, 1, '2023-08-13 21:47:56'),
(28, 1, 3, 1, 1, '2023-08-13 21:54:55'),
(29, 1, 3, 1, 1, '2023-08-13 21:54:55'),
(30, 1, 3, 1, 1, '2023-08-13 21:54:55'),
(31, 1, 3, 1, 1, '2023-08-13 21:54:57'),
(32, 1, 3, 1, 1, '2023-08-13 21:54:57'),
(33, 1, 3, 1, 1, '2023-08-13 21:54:57'),
(34, 1, 3, 1, 1, '2023-08-13 21:55:43'),
(35, 1, 3, 1, 1, '2023-08-13 21:55:43'),
(36, 1, 3, 1, 1, '2023-08-13 21:55:43'),
(37, 1, 3, 1, 1, '2023-08-13 21:56:13'),
(38, 1, 3, 1, 1, '2023-08-13 21:56:13'),
(39, 1, 3, 1, 1, '2023-08-13 21:56:13'),
(40, 1, 3, 1, 1, '2023-08-13 21:56:15'),
(41, 1, 3, 1, 1, '2023-08-13 21:56:15'),
(42, 1, 3, 1, 1, '2023-08-13 21:56:15'),
(43, 1, 3, 1, 1, '2023-08-13 21:56:16'),
(44, 1, 3, 1, 1, '2023-08-13 21:56:16'),
(45, 1, 3, 1, 1, '2023-08-13 21:56:16'),
(46, 1, 3, 1, 1, '2023-08-13 22:05:46'),
(47, 1, 3, 63, 1, '2023-08-13 22:06:46'),
(48, 1, 3, 55, 1, '2023-08-13 22:17:56'),
(49, 1, 3, 55, 1, '2023-08-13 22:21:33'),
(50, 1, 3, 55, 1, '2023-08-13 22:31:59'),
(51, 1, 3, 55, 1, '2023-08-13 22:32:00'),
(52, 1, 3, 55, 1, '2023-08-13 22:32:09'),
(53, 1, 3, 55, 1, '2023-08-13 22:32:10'),
(54, 1, 3, 55, 1, '2023-08-13 22:32:10'),
(55, 1, 3, 55, 1, '2023-08-13 22:32:10'),
(56, 1, 3, 55, 1, '2023-08-13 22:32:12'),
(57, 1, 3, 55, 1, '2023-08-13 22:49:29'),
(58, 1, 3, 55, 1, '2023-08-13 22:52:52'),
(59, 1, 3, 63, 1, '2023-08-13 22:57:27'),
(60, 1, 3, 54, 1, '2023-08-13 23:35:01'),
(61, 0, 3, 54, 1, '2023-08-13 23:36:01'),
(62, 0, 3, 63, 1, '2023-08-13 23:54:57'),
(63, 1, 3, 64, 1, '2023-08-13 23:54:57'),
(64, 0, 3, 55, 1, '2023-08-13 23:54:57'),
(65, 0, 3, 64, 1, '2023-08-14 12:32:41'),
(66, 1, 3, 63, 1, '2023-08-14 12:32:41'),
(67, 0, 3, 63, 1, '2023-08-14 12:36:00'),
(68, 1, 3, 64, 1, '2023-08-14 12:36:00'),
(69, 0, 3, 63, 1, '2023-08-14 12:36:13'),
(70, 1, 3, 64, 1, '2023-08-14 12:36:13'),
(71, 0, 3, 64, 1, '2023-08-14 12:46:33'),
(72, 1, 3, 63, 1, '2023-08-14 12:46:33'),
(73, 0, 3, 64, 1, '2023-08-14 12:46:38'),
(74, 1, 3, 63, 1, '2023-08-14 12:46:38'),
(75, 0, 3, 63, 1, '2023-08-14 12:54:04'),
(76, 1, 3, 64, 1, '2023-08-14 12:54:04'),
(77, 0, 3, 63, 1, '2023-08-14 12:54:15'),
(78, 1, 3, 64, 1, '2023-08-14 12:54:15'),
(79, 1, 3, 64, 1, '2023-08-14 12:54:16'),
(80, 0, 3, 63, 1, '2023-08-14 12:54:16'),
(81, 0, 3, 63, 1, '2023-08-14 12:54:17'),
(82, 1, 3, 64, 1, '2023-08-14 12:54:17'),
(83, 0, 3, 63, 1, '2023-08-14 12:54:19'),
(84, 1, 3, 64, 1, '2023-08-14 12:54:19'),
(85, 0, 3, 63, 1, '2023-08-14 12:54:20'),
(86, 1, 3, 64, 1, '2023-08-14 12:54:20'),
(87, 0, 3, 63, 1, '2023-08-14 12:54:21'),
(88, 1, 3, 64, 1, '2023-08-14 12:54:21'),
(89, 0, 3, 63, 1, '2023-08-14 12:54:22'),
(90, 1, 3, 64, 1, '2023-08-14 12:54:22'),
(91, 1, 3, 64, 1, '2023-08-14 12:54:22'),
(92, 0, 3, 63, 1, '2023-08-14 12:54:22'),
(93, 0, 3, 63, 1, '2023-08-14 12:54:23'),
(94, 1, 3, 64, 1, '2023-08-14 12:54:23'),
(95, 0, 3, 63, 1, '2023-08-14 12:54:24'),
(96, 1, 3, 64, 1, '2023-08-14 12:54:24'),
(97, 0, 3, 63, 1, '2023-08-14 12:54:24'),
(98, 1, 3, 64, 1, '2023-08-14 12:54:24'),
(99, 1, 3, 64, 1, '2023-08-14 13:05:24'),
(100, 0, 3, 63, 1, '2023-08-14 13:05:24'),
(101, 1, 3, 64, 1, '2023-08-14 13:11:34'),
(102, 0, 3, 63, 1, '2023-08-14 13:11:34'),
(103, 1, 3, 65, 1, '2023-08-14 16:46:02'),
(104, 0, 3, 63, 1, '2023-08-14 16:46:02'),
(105, 1, 3, 68, 1, '2023-10-23 14:33:10'),
(106, 0, 3, 67, 1, '2023-10-23 14:33:10'),
(107, 1, 3, 70, 7, '2023-10-25 15:40:54'),
(108, 0, 3, 75, 7, '2023-10-25 16:31:09'),
(109, 1, 3, 76, 7, '2023-10-25 16:31:09'),
(110, 1, 3, 70, 7, '2023-10-26 12:14:25'),
(111, 1, 3, 70, 7, '2023-10-26 12:14:31'),
(112, 1, 3, 70, 7, '2023-10-26 12:15:08'),
(113, 1, 3, 70, 7, '2023-10-26 12:16:04'),
(114, 1, 3, 1, 7, '2023-10-26 12:16:48'),
(115, 0, 3, 0, 7, '2023-10-26 12:20:47'),
(116, 1, 3, 0, 7, '2023-10-26 12:20:47'),
(117, 0, 3, 76, 7, '2023-10-26 12:26:03'),
(118, 1, 3, 75, 7, '2023-10-26 12:26:03'),
(119, 0, 3, 74, 7, '2023-10-26 12:28:12'),
(120, 1, 3, 75, 7, '2023-10-26 12:28:12'),
(121, 0, 3, 75, 7, '2023-10-26 12:33:36'),
(122, 1, 3, 74, 7, '2023-10-26 12:33:36'),
(123, 0, 3, 75, 7, '2023-10-26 12:35:52'),
(124, 1, 3, 76, 7, '2023-10-26 12:35:52'),
(125, 0, 3, 76, 7, '2023-10-26 12:37:24'),
(126, 1, 3, 75, 7, '2023-10-26 12:37:24'),
(127, 1, 3, 76, 7, '2023-10-26 12:40:46'),
(128, 0, 3, 75, 7, '2023-10-26 12:40:46'),
(129, 1, 3, 1, 7, '2023-10-26 12:42:15'),
(130, 1, 3, 1, 7, '2023-10-27 14:13:39'),
(131, 1, 3, 1, 7, '2023-10-27 14:14:21'),
(132, 1, 3, 1, 7, '2023-10-27 14:14:51'),
(133, 1, 3, 1, 7, '2023-10-27 14:15:40'),
(134, 1, 3, 1, 7, '2023-10-27 14:16:00'),
(135, 1, 3, 1, 7, '2023-10-27 14:16:12'),
(136, 1, 3, 75, 7, '2023-10-27 14:33:01'),
(137, 1, 3, 75, 7, '2023-10-27 14:35:21'),
(138, 1, 3, 75, 7, '2023-10-27 14:36:37'),
(139, 1, 3, 1, 7, '2023-10-27 14:41:05'),
(140, 1, 3, 1, 7, '2023-10-27 15:05:01'),
(141, 1, 3, 1, 7, '2023-10-27 15:40:03'),
(142, 0, 3, 1, 7, '2023-10-27 15:40:08'),
(143, 1, 3, 78, 7, '2023-10-28 12:58:27'),
(144, 0, 3, 75, 7, '2023-10-28 12:58:27');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `Servers`
--

CREATE TABLE `Servers` (
  `id` int(11) NOT NULL,
  `server_name` varchar(40) NOT NULL,
  `password` varchar(60) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Gegevens worden geëxporteerd voor tabel `Servers`
--

INSERT INTO `Servers` (`id`, `server_name`, `password`) VALUES
(1, 'Henk', 'Steen');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `Users`
--

CREATE TABLE `Users` (
  `id` int(11) NOT NULL,
  `name` varchar(40) NOT NULL,
  `email` varchar(60) NOT NULL,
  `password` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_spanish2_ci NOT NULL,
  `datetime` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Gegevens worden geëxporteerd voor tabel `Users`
--

INSERT INTO `Users` (`id`, `name`, `email`, `password`, `datetime`) VALUES
(70, 'joey1', 'joey@hotmail.nl', 'joey1', '2023-10-24 13:48:20'),
(71, 'joey2', 'joey@gmail.com', 'joey2', '2023-10-24 13:51:57'),
(72, 'joey3', 'joey3@gmail.com', 'joey3', '2023-10-25 14:18:47'),
(73, 'jogchum1', 'jogchum@hotmail.com', 'jogchum1', '2023-10-25 14:41:48'),
(74, 'Unity1', 'test@gmail.com', 'Unity1', '2023-10-25 15:15:15'),
(75, 'Unity2', 'test@hotmail.com', 'Unity2', '2023-10-25 15:16:51'),
(76, 'Unity3', 'test@gmail.com', 'Unity3', '2023-10-25 15:17:29'),
(77, 'Unity4', 'Unity4@gmail.nl', 'Unity4', '2023-10-27 14:32:12'),
(78, '123', 'supertest@gmail.com', '123', '2023-10-28 12:57:36');

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `Games`
--
ALTER TABLE `Games`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `LastPlayedGames`
--
ALTER TABLE `LastPlayedGames`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `Score`
--
ALTER TABLE `Score`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `Servers`
--
ALTER TABLE `Servers`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `Games`
--
ALTER TABLE `Games`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT voor een tabel `Score`
--
ALTER TABLE `Score`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=145;

--
-- AUTO_INCREMENT voor een tabel `Servers`
--
ALTER TABLE `Servers`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT voor een tabel `Users`
--
ALTER TABLE `Users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=79;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
