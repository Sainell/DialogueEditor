update 'dialogue_node' SET Npc_text ='Я тестовый NPC 4, давно наблюдаю за тобой...' where Npc_id = 4 and Node_id = 0; 
update 'dialogue_answers' SET Answer_text='О, привет тебе!', To_node ='1', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '25'; 
update 'dialogue_answers' SET Answer_text='Зачем ты наблюдаешь?', To_node ='2', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '26'; 
update 'dialogue_node' SET Npc_text ='Привет? Ну привет...хотя пожалуй нет.' where Npc_id = 4 and Node_id = 1; 
update 'dialogue_answers' SET Answer_text='Нет? Почему нет?', To_node ='4', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '27'; 
update 'dialogue_node' SET Npc_text ='Я отказываюсь отвечать на данный вопрос.' where Npc_id = 4 and Node_id = 2; 
update 'dialogue_answers' SET Answer_text='Но почему?', To_node ='4', Quest_ID='0', End_dialogue='0', Start_quest='0', End_quest='0'   where Id = '29'; 
update 'dialogue_node' SET Npc_text ='Пока? Пока.' where Npc_id = 4 and Node_id = 3; 
update 'dialogue_node' SET Npc_text ='...' where Npc_id = 4 and Node_id = 4; 
update 'dialogue_answers' SET Answer_text='Странный ты.', To_node ='0', Quest_ID='0', End_dialogue='1', Start_quest='0', End_quest='0'   where Id = '28'; 