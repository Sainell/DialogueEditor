insert into 'dialogue_answers' (Npc_id, Node_ID, Answer_text, To_node, End_dialogue) values ('3','0','answer','0','0'); 
update 'dialogue_node' SET Npc_text ='Здрасьте. Какими судьбами?' where Npc_id = 3 and Node_id = 0; 
update 'dialogue_answers' SET Answer_text='Привет, да вот мимо проходил.', To_node ='1', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '21'; 
update 'dialogue_answers' SET Answer_text='Ой, забыл...пока.', To_node ='0', Quest_ID='0', End_dialogue='1', Start_quest='0', End_quest='0'   where Id = '22'; 
update 'dialogue_answers' SET Answer_text='test2 answer new', To_node ='2', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '30'; 
update 'dialogue_node' SET Npc_text ='Где был? Что видел?' where Npc_id = 3 and Node_id = 1; 
update 'dialogue_answers' SET Answer_text='Меня отправил к тебе твой братец! Он просит передать тебе привет.', To_node ='2', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '23'; 
update 'dialogue_node' SET Npc_text ='Ооо... круто! Спасибо. Увидешь его еще раз, передавай и мой привет. Удачи.' where Npc_id = 3 and Node_id = 2; 
update 'dialogue_answers' SET Answer_text='Хорошо, обязательно передам.', To_node ='0', Quest_ID='1', End_dialogue='1', Start_quest='0', End_quest='1'   where Id = '24'; 
insert into 'dialogue_node' (Npc_id, Node_ID, Npc_text) values ('3','3','npc text') ; 
insert into 'dialogue_answers' (Npc_id, Node_ID, Answer_text, To_node, End_dialogue) values ('3','3','answer','0','0'); 
update 'dialogue_node' SET Npc_text ='Здрасьте. Какими судьбами?' where Npc_id = 3 and Node_id = 0; 
update 'dialogue_answers' SET Answer_text='Привет, да вот мимо проходил.', To_node ='1', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '21'; 
update 'dialogue_answers' SET Answer_text='Ой, забыл...пока.', To_node ='0', Quest_ID='0', End_dialogue='1', Start_quest='0', End_quest='0'   where Id = '22'; 
update 'dialogue_answers' SET Answer_text='test2 answer new', To_node ='2', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '30'; 
update 'dialogue_node' SET Npc_text ='Где был? Что видел?' where Npc_id = 3 and Node_id = 1; 
update 'dialogue_answers' SET Answer_text='Меня отправил к тебе твой братец! Он просит передать тебе привет.', To_node ='2', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '23'; 
update 'dialogue_node' SET Npc_text ='Ооо... круто! Спасибо. Увидешь его еще раз, передавай и мой привет. Удачи.' where Npc_id = 3 and Node_id = 2; 
update 'dialogue_answers' SET Answer_text='Хорошо, обязательно передам.', To_node ='0', Quest_ID='1', End_dialogue='1', Start_quest='0', End_quest='1'   where Id = '24'; 
update 'dialogue_node' SET Npc_text ='npc text' where Npc_id = 3 and Node_id = 3; 
update 'dialogue_answers' SET Answer_text='test answer new node ', To_node ='1', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '31'; 
update 'dialogue_node' SET Npc_text ='Здрасьте. Какими судьбами?' where Npc_id = 3 and Node_id = 0; 
update 'dialogue_answers' SET Answer_text='Привет, да вот мимо проходил.', To_node ='1', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '21'; 
update 'dialogue_answers' SET Answer_text='Ой, забыл...пока.', To_node ='0', Quest_ID='0', End_dialogue='1', Start_quest='0', End_quest='0'   where Id = '22'; 
update 'dialogue_answers' SET Answer_text='test2 answer new', To_node ='3', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '30'; 
update 'dialogue_node' SET Npc_text ='Где был? Что видел?' where Npc_id = 3 and Node_id = 1; 
update 'dialogue_answers' SET Answer_text='Меня отправил к тебе твой братец! Он просит передать тебе привет.', To_node ='2', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '23'; 
update 'dialogue_node' SET Npc_text ='Ооо... круто! Спасибо. Увидешь его еще раз, передавай и мой привет. Удачи.' where Npc_id = 3 and Node_id = 2; 
update 'dialogue_answers' SET Answer_text='Хорошо, обязательно передам.', To_node ='0', Quest_ID='1', End_dialogue='1', Start_quest='0', End_quest='1'   where Id = '24'; 
update 'dialogue_node' SET Npc_text ='npc text' where Npc_id = 3 and Node_id = 3; 
update 'dialogue_answers' SET Answer_text='test answer new node ', To_node ='1', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '31'; 
