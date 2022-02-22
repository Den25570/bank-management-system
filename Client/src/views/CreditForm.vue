<template>
    <b-container class="my-4">
        <h2 class="credit-account-header">Форма заключения кредита</h2>
        <div class="credit-account-form p-3">
            <div class="credit-account-form-fields">
                <div class="credit-account-form-fields-name">
                    <b-row class="">
                        <b-col sm="2"><label class="font-weight-bold">Идентиф. код</label></b-col>
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
                        <b-col sm="2"><label class="font-weight-bold">ФИО</label></b-col>
                        <b-col sm="3" class="d-flex align-items-center"><b-form-input v-model="selectedClient.firstname"></b-form-input></b-col>
                        <b-col sm="3" class="d-flex align-items-center"><b-form-input v-model="selectedClient.lastname"></b-form-input></b-col>
                        <b-col sm="3" class="d-flex align-items-center"><b-form-input v-model="selectedClient.middlename"></b-form-input></b-col>
                    </b-row>

                </div>
                <div class="credit-account-form-fields-amount my-2">
                    <b-row>
                        <b-col sm="2"><label class="font-weight-bold">Данные кредита</label></b-col>
                        <b-col sm="4"><label>Вид Кредита</label></b-col>
                        <b-col sm="2"><label>Тип Кредита</label></b-col>
                        <b-col sm="2"><label>Сумма</label></b-col>
                        <b-col sm="1"><label>Валюта</label></b-col>
                        <b-col sm="1"><label>Процент</label></b-col>
                    </b-row>
                    <b-row>
                        <b-col sm="2"></b-col>
                        <b-col sm="4" class="d-flex align-items-center"><b-form-select class="form-input" :options="creditCodes" v-model="credit.creditCode"></b-form-select></b-col>
                        <b-col sm="2" class="d-flex align-items-center"><b-form-select class="form-input" :options="creditTypes" v-model="credit.creditTypeId" :state="creditTypeState"></b-form-select></b-col>
                        <b-col sm="2" class="d-flex align-items-center"><b-form-input min="10" max="10000000" v-model="credit.creditAmount" :state="creditAmountState" type="number"></b-form-input></b-col>
                        <b-col sm="1" class="d-flex align-items-center"><b-form-select :options="currencies" v-model="credit.currencyId" :state="currencyIdState"></b-form-select></b-col>
                        <b-col sm="1" class="d-flex align-items-center"><b-form-input min="0" max="100" v-model="credit.percent" :state="percentState" type="number"></b-form-input></b-col>
                    </b-row>
                </div>

                <div class="credit-account-form-fields-date my-2">
                    <b-row>
                        <b-col sm="2"></b-col>
                        <b-col sm="3"><label>Начало</label></b-col>
                        <b-col sm="3"><label>Окончание</label></b-col>
                        <b-col sm="3"><label>Срок (дни)</label></b-col>
                    </b-row>
                    <b-row>
                        <b-col sm="2"></b-col>
                        <b-col sm="3" class="d-flex align-items-center"><b-form-input v-model="credit.startDate" :state="startDateState" type="date"></b-form-input></b-col>
                        <b-col sm="3" class="d-flex align-items-center"><b-form-input v-model="credit.endDate" :state="endDateState" type="date"></b-form-input></b-col>
                        <b-col sm="3" class="d-flex align-items-center"><b-form-input disabled v-model="contractTerm" :state="contractTermState" type="number"></b-form-input></b-col>
                    </b-row>
                </div>
            </div>
            <div class="d-flex justify-content-end mt-4 border-top py-3">
                <b-button variant="success" @click="CreateCredit" :disabled="triedUpdateOrCreate && !state">Оформить Кредит</b-button>
            </div>
        </div>
    </b-container>
</template>

<script>
import { createCredit, getCreditCodes } from "../services/credits-api.js"
import { searchClientsByPassport } from '../services/clients-api.js'
import { Currencies, CreditTypes} from '../data/data.js'
export default {
    name: "CreditForm",
    data() {
        return {
            creditCodes: [],
            clients: [],
            selectedClient: {
                passportIdNumber: "",
                middlename: "",
                lastname: "",
                firstname: "",
                id: null,
            },
            credit: {
                clientId: null,
                name: "",
                creditTypeId: 1,
                creditAmount: 0,
                currencyId: 1,
                creditCode: null,
                percent: 0,
                startDate: new Date().toISOString().substring(0, new Date().toISOString().indexOf("T")),
                endDate: new Date().toISOString().substring(0, new Date().toISOString().indexOf("T")),
                contractTerm: 0,
            },
            triedUpdateOrCreate: false,
            currencies: Currencies.map(c => {return {value: c.Id, text: c.Name}}),
            creditTypes: CreditTypes.map(c => {return {value: c.Id, text: c.Name}}),
        }
    },
    async mounted() {
        const codes = await this.GetCreditCodes()
        this.creditCodes = codes.map(c => {return {value: c.id, text: c.description}});
    },
    computed: {
        contractTerm() {
            return (new Date(this.credit.endDate).getTime() - new Date(this.credit.startDate).getTime())/ (1000 * 3600 * 24);
        },
        state() {
            return this.passportIdNumberState && this.percentState && this.endDateState && this.startDateState 
            && this.endDateState && this.contractTermState && this.creditAmountState;
        },
        passportIdNumberState() {
            return this.triedUpdateOrCreate ? !!this.selectedClient.id : null;
        },
        clientIdState() {
            return true;
        },
        creditTypeState() {
            return true;
        },
        creditAmountState() {
            return this.triedUpdateOrCreate ? this.credit.creditAmount >= 10: null;
        },
        currencyIdState() {
            return true; 
        },
        percentState() {
            return  this.triedUpdateOrCreate ? this.credit.percent > 0 && this.credit.percent <= 100 : null;
        },
        startDateState() {
            return this.triedUpdateOrCreate ? Math.floor(new Date(this.credit.startDate).getTime()/ (1000 * 3600 * 24)) >= Math.floor(new Date().getTime() / (1000 * 3600 * 24)): null;
        },
        endDateState() {
            return this.triedUpdateOrCreate ? (this.credit.endDate >= this.credit.startDate) : null;
        },
        contractTermState() {
            return this.triedUpdateOrCreate ? this.contractTerm > 31: null;
        },
    },
    methods: {
        CreateCredit() {
            this.triedUpdateOrCreate = true;
            if (this.state) {
                this.credit.clientId = this.selectedClient.id;
                this.credit.name = `${this.selectedClient.firstname} ${this.selectedClient.lastname} ${this.selectedClient.middlename}`
                createCredit(this.credit).then(data => {
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
        },
        GetCreditCodes: async () => {
            return await getCreditCodes();
        },
    }
}
</script>

<style scoped>
.credit-account-header {
    text-align: left;
}
.font-weight-bold {
    font-weight: bold;
}
.credit-account-form {
    text-align: left;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    border: 1px solid lightgray;
    border-radius: 5px;
}
</style>