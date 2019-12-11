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

--
-- Файл сгенерирован с помощью SQLiteStudio v3.2.1 в Чт дек 12 00:13:39 2019
--
-- Использованная кодировка текста: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Таблица: dialogue_answers
DROP TABLE IF EXISTS dialogue_answers;
CREATE TABLE dialogue_answers (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Node_ID INTEGER, Answer_text TEXT, To_node INTEGER, End_dialogue BOOLEAN, Npc_id INTEGER REFERENCES npc (id), Start_quest INTEGER DEFAULT (0), End_quest INTEGER DEFAULT (0), Quest_ID INTEGER DEFAULT (0), Task_quest INTEGER DEFAULT (0));
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (1, 0, 'Я рыцарь! Бойся меня.', 1, 0, 1, 0, 0, 0, 1);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (2, 0, 'Да так никто, мимо проходил, а что?', 2, 0, 1, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (3, 0, 'Это не имеет значения, какое тебе дело до меня?', 3, 0, 1, 0, 0, 0, 1);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (4, 1, 'Вполне серьезно, не видно что-ли?', 0, 1, 1, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (5, 1, 'Успакойся, это шутка. Ты что-то хотел?', 2, 0, 1, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (6, 4, 'Хорошо, я обязательно передам ему привет.', 0, 0, 1, 1, 0, 1, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (7, 4, 'Знаешь, я передумал. Пока.', 0, 1, 1, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (8, 2, 'Нет, не хочу.', 0, 1, 1, 1, 0, 1, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (9, 2, 'Да, я помогу тебе, какое дело?', 4, 0, 1, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (10, 3, 'Ну тогда пока.', 3, 1, 1, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (11, 0, 'Привет, все хорошо.', 1, 0, 2, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (12, 0, 'Нормально, а как твои дела?', 2, 0, 2, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (13, 0, 'Ты кто такой и почему меня об этом спрашиваешь?', 3, 0, 2, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (14, 0, 'Не хочу с тобой разговаривать, пока.', 4, 0, 2, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (15, 1, 'Ага!', 0, 1, 2, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (16, 3, 'Нет, не узнал. Странный ты, я пошел.', 0, 1, 2, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (17, 3, 'Узнал. Кажется что-то такое припоминаю.', 5, 0, 2, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (18, 4, 'Может быть, я подумаю над этим.', 0, 1, 2, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (19, 2, 'И что видно?', 6, 0, 2, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (20, 2, 'Тут пока-что не особо есть что посмотреть, работаем над этим.', 7, 0, 2, 1, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (21, 0, 'Привет, да вот мимо проходил.', 1, 0, 3, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (22, 0, 'Ой, забыл...пока.', 0, 1, 3, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (23, 1, 'Меня отправил к тебе твой братец! Он просит передать тебе привет.', 2, 0, 3, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (24, 2, 'Хорошо, обязательно передам.', 0, 1, 3, 0, 1, 1, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (25, 0, 'О, привет тебе!', 1, 0, 4, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (26, 0, 'Зачем ты наблюдаешь?', 2, 0, 4, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (27, 1, 'Нет? Почему нет?', 4, 0, 4, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (28, 4, 'Странный ты.', 0, 1, 4, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (29, 2, 'Но почему?', 4, 0, 4, 0, 0, 0, 0);
INSERT INTO dialogue_answers (Id, Node_ID, Answer_text, To_node, End_dialogue, Npc_id, Start_quest, End_quest, Quest_ID, Task_quest) VALUES (31, 3, 'test answer new node ', 1, 0, 3, 0, 0, 0, 0);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;

