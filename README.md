﻿### Test Task #1 for Sunday Game Studio

Реализован весь требуемый функционал.

Все мною прописанное/созданное находится в папке "**Assets/Project/**".

Есть возможность "вернуться в прошлую сцену" с помощью специальных в Android кнопок и жестов,
предположительно точно также на iOS, но проверить нет физической возможности.

Переход **Menu -> Gallery** подгружается через _LoadMode.Single_,
но переход из **Gallery -> View (Просмотр картинки)** идет через  _LoadMode.Additive_, 
дабы не терять уже загруженные данные и положение экрана.