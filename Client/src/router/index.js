import Vue from 'vue'
import VueRouter from 'vue-router'
import ClientsList from '../views/ClientsList.vue'
import CreateClient from '../views/CreateClient.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/clients',
    name: 'Clients',
    component: ClientsList,
  },
  {
    path: '/new',
    name: 'CreateClient',
    component: CreateClient,
  }
]

const router = new VueRouter({
  routes
})

export default router
