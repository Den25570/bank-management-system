<template>
  <div id="app">
    <div class="app-nav">
      <b-dropdown variant="primary" text="Клиенты" class="text-dark m-md-2">
        <b-dropdown-item><router-link class="text-dark" to="/clients">Список</router-link></b-dropdown-item>
        <b-dropdown-item><router-link class="text-dark" to="/register-client">Добавить клиента</router-link></b-dropdown-item>
      </b-dropdown>
      <b-dropdown id="dropdown-1" variant="primary" text="Депозиты" class="m-md-2">
        <b-dropdown-item><router-link class="text-dark" to="/deposits">Список</router-link></b-dropdown-item>
        <b-dropdown-item><router-link class="text-dark" to="/register-deposit">Форма заключения депозита</router-link></b-dropdown-item>
      </b-dropdown>
      <b-dropdown id="dropdown-1" variant="primary" text="Кредиты" class="m-md-2">
        <b-dropdown-item><router-link class="text-dark" to="/credits">Список</router-link></b-dropdown-item>
        <b-dropdown-item><router-link class="text-dark" to="/register-credit">Форма заключения кредита</router-link></b-dropdown-item>
      </b-dropdown>
      <b-dropdown id="dropdown-2" variant="primary" text="Счета" class="m-md-2">
        <b-dropdown-item><router-link class="text-dark" to="/accounts">Список</router-link></b-dropdown-item>
      </b-dropdown>
    </div>
    <router-view/>
  </div>
</template>

<style>
  button.btn-primary {
    background-color: transparent !important; 
    border-color: transparent !important;
  }
  button.btn-check:focus + button.btn-primary, button.btn-primary:focus {
    color: #fff;
    background-color: transparent !important; 
    border-color: transparent !important;
    box-shadow: 0 0 0 0.25rem rgb(49 132 253 / 50%);
}
  p {
    padding: 0;
    margin-bottom: 0 !important;
  }
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
  }

  .app-nav {
    display: flex;
    padding: 10px;
    background-color: #2c3e50;
  }

  .app-nav a {
    color: white;
  }

  .app-nav a.router-link-exact-active {
    text-decoration: underline;
    font-weight: bold;
  }

  .text-dark {
    color: black !important;
  }

  .custom-select {
    display: block;
    width: 100%;
    padding: 0.375rem 0.75rem;
    font-size: 1rem;
    font-weight: 400;
    line-height: 1.5;
    color: #212529;
    background-color: #fff;
    background-clip: padding-box;
    border: 1px solid #ced4da;
    border-radius: 0.25rem;
    transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
  }
</style>
