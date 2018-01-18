﻿# Тестовое задание
<p>VS 2015 (ASP.NET MVC, EF), MS SQL SERVER 2012, требуется создать БД DrinkVendingMachine, объекты накатятся миграцией</p>

<p>
Веб-приложение имитирует работу автомата по продаже напитков. Напитки представлены названием, картинкой и стоимостью.
Автомат принимает монеты номиналом в 1, 2, 5, 10 рублей. Предоставляет возможность купить напитки, внеся сумму, равную или превышающую их стоимость (в случае превышения стоимости автомат сдает сдачу).</p>

<p>
Приложение имеет пользовательский и административный интерфейс. Пользовательский интерфейс предполагает покупку напитков: внесение суммы монетами, используя кнопки с номиналом монет (1, 2, 5, 10), выбор напитков (есть возможность покупки нескольких напитков перед получением сдачи), используя изображения напитков, получение сдачи. Доступ в пользовательский интерфейс свободен. Административный интерфейс предоставляет инструменты управления автоматом (добавление, удаление напитков, изменение их количества, стоимости и изображения, изменение количества монет, блокирование приема тех или иных монет). Доступ в административный интерфейс по секретному ключу, который передаётся как параметр в адресной строке (http://localhost:2257/Account/Login?password=adminpassword).
</p>


