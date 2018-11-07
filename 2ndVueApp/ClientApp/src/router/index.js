import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Earnings from '@/components/Earnings'
import BootstrapVue from 'bootstrap-vue'

Vue.use(Router);
Vue.use(BootstrapVue);
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/earnings',
      name: 'Earnings',
      component: Earnings
    }
  ]
})
