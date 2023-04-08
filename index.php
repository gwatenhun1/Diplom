<?php
$host = 'localhost';
$user = 'root';
$password = '';
$database = 'rezume';
$port = '3306';

$connection = mysqli_connect($host, $user, $password, $database, $port);

if (!$connection) {
  die("Ошибка подключения: " . mysqli_connect_error());
}

if (isset($_POST['submit'])) {
  $gender = $_POST['gender'];
  $name = $_POST['name'];
  $surname = $_POST['surname'];
  $birthday = $_POST['birthday'];
  $country = $_POST['country'];
  $city = $_POST['city'];
  $email = $_POST['email'];
  $phone = $_POST['phone'];
  $comments = $_POST['comments'];

  $query = "INSERT INTO users (gender, name, surname, birthday, country, city, email, phone, comments) VALUES ('$gender', '$name', '$surname', '$birthday', '$country', '$city', '$email', '$phone', '$comments')";

  if (!mysqli_query($connection, $query)) {
    die('Ошибка: ' . mysqli_error($connection));
  } else {
    echo '<p>Данные успешно отправлены!</p>';
  }
}

mysqli_close($connection);
?>
<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<title>Мой сайт</title>
<style>
    /* apply styles to form elements */
    .form-group {
      margin-bottom: 1rem;
    }

    label {
      display: block;
      margin-bottom: 0.5rem;
    }

    input,
    select,
    textarea {
      width: 100%;
      padding: 0.5rem;
      font-size: 1rem;
      border: 1px solid #ccc;
      border-radius: 3px;
      box-sizing: border-box;
      margin-top: 0.5rem;
    }

    button[type="submit"] {
      background-color: #007bff;
      color: #fff;
      border: none;
      border-radius: 3px;
      padding: 0.5rem 1rem;
      font-size: 1rem;
      cursor: pointer;
    }

    button[type="submit"]:hover {
      background-color: #0069d9;
    }

    /* apply styles to form headings */
    h2 {
      margin-top: 2rem;
      margin-bottom: 1rem;
    }
    </style>
</head>
<body>
    <form method="post">

        <h2>Персональная информация</h2>

        <div class="form-group">
        <label for="gender">Выберите пол:</label>
        <select id="gender" name="gender">
        <option value="Мужской">Мужской</option>
        <option value="Женский">Женский</option>
        </select>
        </div>

        <div class="form-group">
        <label for="name">Введите своё имя:</label>
        <input type="text" id="name" name="name">
        </div>

        <div class="form-group">
        <label for="surname">Введите свою фамилию:</label>
        <input type="text" id="surname" name="surname">
        </div>

        <div class="form-group">
        <label for="birthday">Введите свою дату рождения:</label>
        <input type="date" id="birthday" name="birthday">
        </div>

        <div class="form-group">
        <label for="country">Выберите страну проживания:</label>
        <select id="country" name="country">
        <option value="Россия">Россия</option>
        <option value="США">США</option>
        <option value="Канада">Канада</option>
        </select>
        </div>

        <div class="form-group">
        <label for="city">Выберите город проживания:</label>
        <select id="city" name="city">
        <option value="Москва">Москва</option>
        <option value="Нью-Йорк">Нью-Йорк</option>
        <option value="Торонто">Торонто</option>
        </select>
        </div>

        <h2>Контактная информация</h2>

        <div class="form-group">
        <label for="email">Введите свой e-mail:</label>
        <input type="email" id="email" name="email">
        </div>

        <div class="form-group">
        <label for="phone">Введите свой номер телефона:</label>
        <input type="tel" id="phone" name="phone">
        </div>

        <h2>Дополнительная информация</h2>

        <div class="form-group">
        <label for="comments">Комментарии:</label>
        <textarea id="comments" name="comments" rows="5" cols="50"></textarea>
        </div>

        <div class="form-group">
        <button type="submit" name="submit">Отправить</button>
        </div>

        </form>
      </body>
</html>
