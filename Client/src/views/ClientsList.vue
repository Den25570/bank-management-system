<template>
  <b-container>
    <div class="clients-list accordion"  role="tablist">
        <div class="clients-list-item shadow-sm p-2 my-2" v-for="client in clients" :key="client.id">
          <div class="clients-list-item-header">
            <div class="clients-list-header-text d-flex flex-start font-weight-bold">
              <p class="mx-1 p-0">{{ client.firstname }}</p><p class="mx-1 p-0">{{ client.lastname }}</p><p class="mx-1 p-0">{{ client.middlename }}</p>
            </div>
            <div v-b-toggle="'accordion-'+client.id" class="clients-list-arrow"></div>
          </div>
            <b-collapse class="clients-list-item-body" :id="`accordion-${client.id}`" visible accordion="my-accordion" role="tabpanel">
              <client-edit :client="client" :isEdit="true"></client-edit>
            </b-collapse>
        </div>
    </div>
  </b-container>
</template>

<script>
import {getClients} from '../services/api.js'
import ClientEdit from "../components/Clients/ClientEdit.vue"
export default {
    name: "ClientList",
    components: {
      ClientEdit
    },
    data() {
      return {
        clients: []
      }
    },
    async mounted() {
      this.clients = await this.GetClients();
    },
    methods: {
      GetClients: () => {
        return getClients()
      },
      OpenItem: () => {

      }
    }
}
</script>

<style>
.clients-list-item-body label {
  text-align: left;
  width: 100%;
  line-height: 20px;
}
.clients-list-item-body .custom-checkbox {
  display: flex;
  justify-content: start;
}
.client-list {
  display: flex;
  flex-direction: column;
}
.clients-list-item {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  border: 1px solid lightgray;
  border-radius: 5px;
}
.clients-list-item-body {
  border-top: 1px solid lightgray;
  padding-top: 10px;
}
.clients-list-item-header {
  display: flex;
  justify-content: space-between;
  font-weight: bold;
}
.clients-list-arrow {
  margin-right: 10px;
  width: 20px;
  height: 20px;
  background-image: url("../assets/down-arrow.svg");
  cursor: pointer;
  -webkit-animation:spin 4s linear infinite;
  -moz-animation:spin 4s linear infinite;
  animation:spin 4s linear infinite;
}
.clients-list-arrow.active {
  transform: rotate(180deg);
}

</style>