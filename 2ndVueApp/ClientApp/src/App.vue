<template>
  <div id="app" style="width:800px;margin-left:auto;margin-right:auto;">
    <router-link class="ui button" to="/earnings">Go to Earnings</router-link>
    <router-link class="ui button" to="/">Go to Home</router-link>
    <br /><br />
    <router-view />
  </div>
</template>

<script>
  import axios from "axios";

export default {
    name: 'App',
    data() {
      return {
      }
    },
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 1.5rem;
}
</style>
