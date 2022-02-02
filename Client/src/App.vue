<template>
  <div id="app">
    <div class="app-nav">
      <router-link to="/clients">Клиенты</router-link>
      <router-link class="mx-2" to="/new">Добавить клиента</router-link>
    </div>
    <router-view/>
  </div>
</template>

<style>
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
</style>
