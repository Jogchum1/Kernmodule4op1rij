-- phpMyAdmin SQL Dump
-- version 4.9.4
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Gegenereerd op: 13 aug 2023 om 23:59
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
(64, 0, 3, 55, 1, '2023-08-13 23:54:57');

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
(1, 'Jogchum', 'jogchum@hotmail.com', 'Af827asF', '2022-05-09 10:25:49'),
(2, 'Justin', 'justin@hotmail.com', 'Jsy16AS7k', '2022-05-09 10:25:49'),
(3, 'Joey', 'joey@hotmail.com', 'Us61SF72', '2022-05-09 10:25:49'),
(25, 'testmetspatie', 'test@gmail.com', 'testesttest', '2022-06-13 10:02:54'),
(26, 'testmetspatie', 'test@gmail.com', 'testesttest', '2022-06-13 10:04:44'),
(39, 'hashTest', 'hash@gmail.com', '$2y$10$aHI6a.DYItgu2s.me.WrQO6lFoBJR2/MAZlqOloJhw69s1aNyJczW', '2022-06-13 10:08:13'),
(40, 'hashTest', 'hash@gmail.com', '$2y$10$8.40N/jWzBG8z18V9kPjqOO.sYTuoEKa6YsiPOu60YpNggFceuj4C', '2022-06-13 10:17:03'),
(41, 'hashTest', 'hash@gmail.com', '$2y$10$d6veAzYjxuGZO5pCop6tBu1plp/JDvy/rhLiZLiuXTBLNnpOTV0cK', '2022-06-13 10:31:11'),
(42, 'hashTest', 'hash@gmail.com', '$2y$10$QcCZdg0AI8oiRj14nzVWuu6H8evqVJNZ3AWEpFP72heqev.h.kyeW', '2023-08-12 21:05:45'),
(43, 'hashTest', 'hash@gmail.com', '$2y$10$nS5/ihHCgHA44fYZT0J.l.n5A4ovTfSD8aMW6LzKOBxlmB1438jC2', '2023-08-12 21:10:32'),
(44, 'hashTest', 'hash@gmail.com', '$2y$10$VurfXpJbb221dFity0PPn.njEVT.ilZ9Mmyfmz7wHmYsfcRBirsIu', '2023-08-12 21:11:30'),
(45, 'hashTest', 'hash@gmail.com', '$2y$10$a4zSlJmNVZi0CXER8yn25ehiNI02Ssirp.ZtCG..FW72UXJCCEBZ.', '2023-08-12 21:11:53'),
(46, 'hashTest', 'hash@gmail.com', '$2y$10$5nDhc4NvtzU8DeeeA/wC/eC8iTBWvDub9BsPH/.kZEaljmIIT7yiG', '2023-08-12 21:12:34'),
(47, 'hashTest', 'hash@gmail.com', '$2y$10$no1Xrnq6HGQd5aqjnXAg9ul4trygxl1a6UIn6h3UdlcbHWFWOHIRW', '2023-08-12 21:13:03'),
(48, 'hashTest', 'hash@gmail.com', '$2y$10$uzbZjGteUHS.4khL2P1tpuZOMQsqpkLyRquEXx61tFattG.pwjZB6', '2023-08-12 21:14:00'),
(49, 'hashTest', 'hash@gmail.com', '$2y$10$4ylpJN2CVv4BC/lGFmnJWO3WRQ0NuHJxlEHmP1Jy6jlU7M/33X3aC', '2023-08-12 21:15:42'),
(50, 'hashTest2', 'hash@gmail.com', '$2y$10$CJlfTxxI2O3ITRWKEJ2qWuRSovh9q6BRMUqc9CLpN7jLtt9XecfRm', '2023-08-12 21:17:41'),
(51, 'test1', 'test2@gmail.com', '$2y$10$wTAPfRwEbNR/ytyH7zAaAu2Uit6WJeygHZ4zMS5KOdx.XmqoEo1cu', '2023-08-13 15:36:43'),
(52, 'FirstUnityTest', 'lalala@gmail.com', '$2y$10$Tad7wcTH4pjOnQn.DjYLbO6RphdXHY7H2SbLIBZay4QzqhQuODFQC', '2023-08-13 15:43:12'),
(53, 'SecondUnityTest', 'unitytest@hotmail.nl', '$2y$10$99swICGGR69SzNiRYivizud0rxZ5zEtD4koL5qK3k3aV2i0EUJX7W', '2023-08-13 16:00:17'),
(54, 'ThirdUnityTest', 'testtest@hotmail.com', '$2y$10$d6uLns8c2QmuUVcmVQaOgOFs0mARyMxWlYTI1oRkVI/8LXf/oIZHq', '2023-08-13 17:37:54'),
(55, 'UnityTry4', 'test@hotmail.be', '$2y$10$RK.946uBTdFgjVT5LmbUXOI8qFxDy.9HsC43JUlSG4qQ66ZEnDGq.', '2023-08-13 17:48:43'),
(56, 'test1', 'test2@gmail.com', '$2y$10$4gUZscjRWRVsa9IanOrVPOgjNn5KCNhLcZ3olkLn4kh1CEBxJJCXC', '2023-08-13 21:19:45'),
(57, 'test1', 'test2@gmail.com', '$2y$10$0PYSabTrpXHGC1zC0W5aS.eRt5L77mvuMwDX0Ty2mC5uAndV0Zhlm', '2023-08-13 21:20:45'),
(58, 'test1', 'test2@gmail.com', '$2y$10$r2riP3X8NKnE3t3YNK3pTOxs/SxZUcCZlUArN/00VeMhhwKi9mIA6', '2023-08-13 21:21:03'),
(59, 'test1', 'test2@gmail.com', '$2y$10$TRec7ca4n7Z1Kt7YlDIRaupFDZJDDdkPZRORKOxFO16yWR68O4xGK', '2023-08-13 21:21:52'),
(60, 'test1', 'test2@gmail.com', '$2y$10$49KVuHm9YUfh4kUgz0sbSeNLeYF9Fh9zygdIHRmuOtOXMKXKHUHnS', '2023-08-13 21:22:22'),
(61, '123', 'lalala@gmail.com', '$2y$10$EklyW4QLzUPOqF9po2SWz.8n5MbzukLD72P.ig18pvya1reUKjtWS', '2023-08-13 21:22:46'),
(62, 'test1', 'test2@gmail.com', '$2y$10$tPhRAJhEVTfQmqCdxxPP4.hbDM0x0wzgkkIYGoW4jyagdFwTvJxCK', '2023-08-13 21:25:16'),
(63, 'UnityTry5', 'test@hotmail.com', '$2y$10$.SJA3fkJRIb24KyON04xUuXu0oAuIT2OThawLpabHRmKOr6DUYusu', '2023-08-13 21:54:27'),
(64, 'UnityTest6', 'test@gmail.com', '$2y$10$ByVySCfaUEjkXzGsY0/.p.gz62IXZ7Q0f63rYlpiRt8e382wEEQCG', '2023-08-13 22:17:32');

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=65;

--
-- AUTO_INCREMENT voor een tabel `Servers`
--
ALTER TABLE `Servers`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT voor een tabel `Users`
--
ALTER TABLE `Users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=65;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
