import Vue from 'vue'
import VueRouter from 'vue-router'
import ClientsList from '../views/ClientsList.vue'
import CreateClient from '../views/CreateClient.vue'
import DepositForm from '../views/DepositForm.vue'
import DepositList from "../views/DepositList.vue"
import CreditForm from '../views/CreditForm.vue'
import CreditList from "../views/CreditList.vue"
import AccountList from "../views/AccountList.vue"

Vue.use(VueRouter)

const routes = [
  {
    path: '/clients',
    name: 'Clients',
    component: ClientsList,
  },
  {
    path: '/register-deposit',
    name: 'DepositForm',
    component: DepositForm,
  },
  {
    path: '/deposits',
    name: 'DepositList',
    component: DepositList,
  },
  {
    path: '/register-credit',
    name: 'CreditForm',
    component: CreditForm,
  },
  {
    path: '/credits',
    name: 'CreditList',
    component: CreditList,
  },
  {
    path: '/accounts',
    name: 'AccountList',
    component: AccountList,
  },
  {
    path: '/register-client',
    name: 'CreateClient',
    component: CreateClient,
  }
]

const router = new VueRouter({
  routes
})

export default router
