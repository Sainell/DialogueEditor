PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Таблица: quest_objectives
DROP TABLE IF EXISTS quest_objectives;
CREATE TABLE quest_objectives (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, QuestId INTEGER REFERENCES quest (Id) ON DELETE CASCADE NOT NULL, Type INTEGER (3) NOT NULL DEFAULT (1), TargetId INTEGER NOT NULL DEFAULT (1), Amount INTEGER NOT NULL DEFAULT (1), IsOptional INTEGER NOT NULL DEFAULT (0));
INSERT INTO quest_objectives (Id, QuestId, Type, TargetId, Amount, IsOptional) VALUES (1, 1, 8, 1, 1, 0);
INSERT INTO quest_objectives (Id, QuestId, Type, TargetId, Amount, IsOptional) VALUES (2, 1, 8, 12, 1, 0);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;


--
-- Файл сгенерирован с помощью SQLiteStudio v3.2.1 в Ср ноя 27 18:57:02 2019
--
-- Использованная кодировка текста: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Таблица: quest
DROP TABLE IF EXISTS quest;
CREATE TABLE quest (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, MinLevel INTEGER DEFAULT (1) NOT NULL, QuestLevel INTEGER DEFAULT (1) NOT NULL, TimeAllowed INTEGER DEFAULT (- 1) NOT NULL, ZoneId INTEGER DEFAULT (1) NOT NULL, RewardExp INTEGER NOT NULL DEFAULT (0), RewardMoney INTEGER NOT NULL DEFAULT (0), StartDialogId INTEGER NOT NULL DEFAULT (0), EndDialogId INTEGER DEFAULT (0) NOT NULL, StartQuestEventType INTEGER NOT NULL DEFAULT (0), EndQuestEventType INTEGER NOT NULL DEFAULT (0), QuestName STRING DEFAULT questName NOT NULL);
INSERT INTO quest (Id, MinLevel, QuestLevel, TimeAllowed, ZoneId, RewardExp, RewardMoney, StartDialogId, EndDialogId, StartQuestEventType, EndQuestEventType) VALUES (1, 1, 1, -1, 1, 0, 0, 6, 22, 1, 1);
INSERT INTO quest (Id, MinLevel, QuestLevel, TimeAllowed, ZoneId, RewardExp, RewardMoney, StartDialogId, EndDialogId, StartQuestEventType, EndQuestEventType) VALUES (2, 1, 1, -1, 1, 0, 0, 10, 20, 1, 1);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;

--
-- Файл сгенерирован с помощью SQLiteStudio v3.2.1 в Ср ноя 27 18:57:34 2019
--
-- Использованная кодировка текста: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Таблица: game_event_types
DROP TABLE IF EXISTS game_event_types;
CREATE TABLE game_event_types (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, game_events STRING);
INSERT INTO game_event_types (id, game_events) VALUES (1, 'DialogAnswerSelect');
INSERT INTO game_event_types (id, game_events) VALUES (2, 'NpcAreaEnter');
INSERT INTO game_event_types (id, game_events) VALUES (3, 'DialogStarted');
INSERT INTO game_event_types (id, game_events) VALUES (4, 'DialogFinished');
INSERT INTO game_event_types (id, game_events) VALUES (5, 'ObjectUsed');
INSERT INTO game_event_types (id, game_events) VALUES (6, 'ItemUsed');
INSERT INTO game_event_types (id, game_events) VALUES (7, 'AreaEnter');
INSERT INTO game_event_types (id, game_events) VALUES (8, 'NpcDie');
INSERT INTO game_event_types (id, game_events) VALUES (9, 'ItemDrop');
INSERT INTO game_event_types (id, game_events) VALUES (10, 'EnemyAreaEnter');
INSERT INTO game_event_types (id, game_events) VALUES (11, 'MidOfDayTimeComes');
INSERT INTO game_event_types (id, game_events) VALUES (12, 'DayTimeChanged');

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;

--
-- Файл сгенерирован с помощью SQLiteStudio v3.2.1 в Ср ноя 27 18:57:51 2019
--
-- Использованная кодировка текста: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Таблица: quest_task_types
DROP TABLE IF EXISTS quest_task_types;
CREATE TABLE quest_task_types (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, type STRING);
INSERT INTO quest_task_types (id, type) VALUES (1, 'KillNpc');
INSERT INTO quest_task_types (id, type) VALUES (2, 'CollectItem');
INSERT INTO quest_task_types (id, type) VALUES (3, 'TalkWithNpc');
INSERT INTO quest_task_types (id, type) VALUES (4, 'UseObject');
INSERT INTO quest_task_types (id, type) VALUES (5, 'FindLocation');
INSERT INTO quest_task_types (id, type) VALUES (6, 'KillEnemyFamily');
INSERT INTO quest_task_types (id, type) VALUES (7, 'UseItem');
INSERT INTO quest_task_types (id, type) VALUES (8, 'SelectAnswer');

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
