delete from 'quest_objectives' where Id =8; 
update 'quest' SET StartDialogId = '6', EndDialogId = '22', StartQuestEventType = '1', EndQuestEventType = '1' where Id = '1'; 
update 'quest_objectives' SET Type='8', TargetId='2', Amount='1',isOptional ='0' where QuestId='1' and Id ='1'; 
update 'quest_objectives' SET Type='1', TargetId='1', Amount='1',isOptional ='0' where QuestId='1' and Id ='2'; 
