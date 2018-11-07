<template>
  <!--<div style="width:800px;margin-left:auto;margin-right:auto;">-->
  <div>
    <h1 class="ui header">Home Page</h1>
    <table class="ui celled table">
      <thead>
        <tr><th>Rule ID</th>
        <th>Name</th>
        <th>Description</th>
        <th>Active?</th>
        <th>Value</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="rule in rules">
          <td>{{rule.ruleId}}</td>
          <td>{{rule.name}}</td>
          <td>{{rule.description}}</td>
          <td>{{rule.isActive}}</td>
          <td>{{rule.value}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
  import axios from "axios";

  export default {
    name: 'home',
    data() {
      return {
        rules: []
      }
    },
    mounted: function () {
      axios
        .get('api/rules')
        .then(response => (this.rules = response.data))
    }
  }
</script>
