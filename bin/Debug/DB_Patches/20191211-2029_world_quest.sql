update 'quest_locale_ru' SET Title = 'test quest', Description = 'Описание тестового квеста'; 
update 'quest' SET StartDialogId = '6', EndDialogId = '24', StartQuestEventType = '1', EndQuestEventType = '1' where Id = '1'; 
update 'quest_objectives' SET Type='8', TargetId='1', Amount='1',isOptional ='0' where QuestId='1' and Id ='1'; 
update 'quest_objectives' SET Type='8', TargetId='12', Amount='1',isOptional ='0' where QuestId='1' and Id ='2'; 
update 'quest_locale_ru' SET Title = 'test quest', Description = 'Описание тестового квеста'; 
update 'quest' SET StartDialogId = '6', EndDialogId = '24', StartQuestEventType = '1', EndQuestEventType = '1' where Id = '1'; 
update 'quest_objectives' SET Type='8', TargetId='1', Amount='1',isOptional ='0' where QuestId='1' and Id ='1'; 
update 'quest_objectives' SET Type='8', TargetId='12', Amount='1',isOptional ='0' where QuestId='1' and Id ='2'; 
