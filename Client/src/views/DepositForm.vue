<template>
    <b-container class="my-4">
        <h2 class="deposit-account-header">Форма заключения депозита</h2>
        <div class="deposit-account-form p-3">
            <div class="deposit-account-form-fields">
                <div class="deposit-account-form-fields-name">
                    <b-row class="">
                        <b-col sm="3"><label class="font-weight-bold">Идентиф. код</label></b-col>
                        <b-col sm="9" class="d-flex align-items-center">
                            <div>
                                <b-form-input list="client-list-id" @keydown="SearchClients()" v-model="selectedClient.passportIdNumber" :state="passportIdNumberState"></b-form-input>
                                <datalist id="client-list-id">
                                    <option v-for="client in clients" :key="client.id">{{ client.passportIdNumber }}</option>
                                </datalist>
                            </div>
                        </b-col>
                    </b-row>
                    <b-row class="my-1">
                        <b-col sm="3"><label class="font-weight-bold">ФИО</label></b-col>
                        <b-col sm="3" class="d-flex align-items-center"><b-form-input v-model="selectedClient.firstname"></b-form-input></b-col>
                        <b-col sm="3" class="d-flex align-items-center"><b-form-input v-model="selectedClient.lastname"></b-form-input></b-col>
                        <b-col sm="3" class="d-flex align-items-center"><b-form-input v-model="selectedClient.middlename"></b-form-input></b-col>
                    </b-row>

                </div>
                <div class="deposit-account-form-fields-amount my-2">
                    <b-row>
                        <b-col sm="3"><label class="font-weight-bold">Данные депозита</label></b-col>
                        <b-col sm="3"><label>Вид Депозита</label></b-col>
                        <b-col sm="2"><label>Сумма</label></b-col>
                        <b-col sm="2"><label>Валюта</label></b-col>
                        <b-col sm="2"><label>Процент</label></b-col>
                    </b-row>
                    <b-row>
                        <b-col sm="3"></b-col>
                        <b-col sm="3" class="d-flex align-items-center"><b-form-select class="form-input" :options="depositTypes" v-model="deposit.depositTypeId" :state="depositTypeState"></b-form-select></b-col>
                        <b-col sm="2" class="d-flex align-items-center"><b-form-input min="10" max="10000000" v-model="deposit.depositAmount" :state="depositAmountState" type="number"></b-form-input></b-col>
                        <b-col sm="2" class="d-flex align-items-center"><b-form-select :options="currencies" v-model="deposit.currencyId" :state="currencyIdState"></b-form-select></b-col>
                        <b-col sm="2" class="d-flex align-items-center"><b-form-input min="0" max="100" v-model="deposit.percent" :state="percentState" type="number"></b-form-input></b-col>
                    </b-row>
                </div>

                <div class="deposit-account-form-fields-date my-2">
                    <b-row>
                        <b-col sm="3"></b-col>
                        <b-col sm="3"><label>Начало</label></b-col>
                        <b-col sm="3"><label>Окончание</label></b-col>
                        <b-col sm="3"><label>Срок (дни)</label></b-col>
                    </b-row>
                    <b-row>
                        <b-col sm="3"></b-col>
                        <b-col sm="3" class="d-flex align-items-center"><b-form-input v-model="deposit.startDate" :state="startDateState" type="date"></b-form-input></b-col>
                        <b-col sm="3" class="d-flex align-items-center"><b-form-input v-model="deposit.endDate" :state="endDateState" type="date"></b-form-input></b-col>
                        <b-col sm="3" class="d-flex align-items-center"><b-form-input disabled v-model="contractTerm" :state="contractTermState" type="number"></b-form-input></b-col>
                    </b-row>
                </div>
            </div>
            <div class="d-flex justify-content-end mt-4 border-top py-3">
                <b-button variant="success" @click="CreateDeposit" :disabled="triedUpdateOrCreate && !state">Создать Депозит</b-button>
            </div>
        </div>
    </b-container>
</template>

<script>
import { createDeposit } from "../services/deposits-api.js"
import { searchClientsByPassport } from '../services/clients-api.js'
import { Currencies, DepositTypes} from '../data/data.js'
export default {
    name: "DepositForm",
    data() {
        return {
            clients: [],
            selectedClient: {
                passportIdNumber: "",
                middlename: "",
                lastname: "",
                firstname: "",
                id: null,
            },
            deposit: {
                clientId: null,
                name: "",
                depositTypeId: -1,
                depositAmount: 0,
                currencyId: 1,
                percent: 0,
                startDate: new Date().toISOString().substring(0, new Date().toISOString().indexOf("T")),
                endDate: new Date().toISOString().substring(0, new Date().toISOString().indexOf("T")),
                contractTerm: 0,
            },
            triedUpdateOrCreate: false,
            currencies: Currencies.map(c => {return {value: c.Id, text: c.Name}}),
            depositTypes: DepositTypes.map(c => {return {value: c.Id, text: c.Name}}),
        }
    },
    computed: {
        contractTerm() {
            return (new Date(this.deposit.endDate).getTime() - new Date(this.deposit.startDate).getTime())/ (1000 * 3600 * 24);
        },
        state() {
            return this.passportIdNumberState && this.percentState && this.endDateState && this.startDateState 
            && this.endDateState && this.contractTermState && this.depositAmountState;
        },
        passportIdNumberState() {
            return this.triedUpdateOrCreate ? !!this.selectedClient.id : null;
        },
        clientIdState() {
            return true;
        },
        depositTypeState() {
            return true;
        },
        depositAmountState() {
            return this.triedUpdateOrCreate ? this.deposit.depositAmount >= 10: null;
        },
        currencyIdState() {
            return true; 
        },
        percentState() {
            return  this.triedUpdateOrCreate ? this.deposit.percent > 0 && this.deposit.percent <= 100 : null;
        },
        startDateState() {
            return this.triedUpdateOrCreate ? Math.floor(new Date(this.deposit.startDate).getTime()/ (1000 * 3600 * 24)) >= Math.floor(new Date().getTime() / (1000 * 3600 * 24)): null;
        },
        endDateState() {
            return this.triedUpdateOrCreate ? (this.deposit.endDate >= this.deposit.startDate) : null;
        },
        contractTermState() {
            return this.triedUpdateOrCreate ? this.contractTerm > 31: null;
        },
    },
    methods: {
        CreateDeposit() {
            this.triedUpdateOrCreate = true;
            if (this.state) {
                this.deposit.clientId = this.selectedClient.id;
                this.deposit.name = `${this.selectedClient.firstname} ${this.selectedClient.lastname} ${this.selectedClient.middlename}`
                createDeposit(this.deposit).then(data => {
                    alert(data)
                })
            }
        },
        SearchClients() {
            if (this.selectedClient.passportIdNumber.length > 3) {
                searchClientsByPassport(this.selectedClient.passportIdNumber).then(data => {
                    this.clients = data;
                    if (this.clients.length == 1 && this.clients[0].passportIdNumber == this.selectedClient.passportIdNumber) {
                        this.selectedClient.middlename = this.clients[0].middlename 
                        this.selectedClient.lastname= this.clients[0].lastname 
                        this.selectedClient.firstname= this.clients[0].firstname 
                        this.selectedClient.id= this.clients[0].id 
                    }
                } )
            }
        }
    }
}
</script>

<style scoped>
.deposit-account-header {
    text-align: left;
}
.font-weight-bold {
    font-weight: bold;
}
.deposit-account-form {
    text-align: left;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    border: 1px solid lightgray;
    border-radius: 5px;
}
</style>