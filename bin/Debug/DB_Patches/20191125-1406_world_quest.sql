insert into 'quest_objectives' (QuestId) values ('1') ; 
delete from 'quest' where Id =1; 
delete from 'quest_objectives' where QuestId =1; 
insert into 'quest_objectives' (QuestId) values ('2') ; 
insert into 'quest_objectives' (QuestId) values ('2') ; 
insert into 'quest' (StartDialogId, EndDialogId, StartQuestEventType, EndQuestEventType) values ('1','1','1','1') ; 
insert into 'quest_objectives' (QuestId) values ('3') ; 
update 'quest' SET StartDialogId = '1', EndDialogId = '1', StartQuestEventType = '1', EndQuestEventType = '1', QuestName='questName' where Id = '3'; 
update 'quest_objectives' SET Type='8', TargetId='3', Amount='1',isOptional ='0' where QuestId='3' and Id ='1'; 
