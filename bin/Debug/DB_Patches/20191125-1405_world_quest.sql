insert into 'quest_objectives' (QuestId) values ('1') ; 
delete from 'quest' where Id =1; 
delete from 'quest_objectives' where QuestId =1; 
insert into 'quest_objectives' (QuestId) values ('2') ; 
insert into 'quest_objectives' (QuestId) values ('2') ; 
insert into 'quest' (StartDialogId, EndDialogId, StartQuestEventType, EndQuestEventType) values ('1','1','1','1') ; 
