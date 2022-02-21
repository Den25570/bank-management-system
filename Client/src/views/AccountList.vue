<template>
    <b-container class="deposits-list my-4">
        <h3 class="deposits-list-header">Cчета</h3>
        <div class="deposits-list-body"> 
            <table>
                <tr>
                    <th>№</th>
                    <th>Название счёта</th>
                    <th>Номер счёта</th>
                    <th>Дебит</th>
                    <th>Кредит</th>
                    <th>Баланс</th>
                    <th>Валюта</th>
                </tr>
                <tr v-for="account in accounts" :key="account.id">
                    <td>{{account.id}}</td>
                    <td>{{account.name}}</td>
                    <td>{{account.accountTypeId}}{{account.individualNumber.toString().padStart(9, '0')}}</td>
                    <td>{{account.debit}}</td>
                    <td>{{account.credit}}</td>
                    <td>{{account.balance}}</td>
                    <td>{{currencies.find(c => c.Id === account.currencyId).Name}}</td>
                </tr>
            </table>
        </div>
        <div class="d-flex">
           <b-button class="my-3" @click="EndBankDay(daysToSkip)">Закрыть день</b-button>
           <b-form-input style="width: 100px; margin: 15px;" v-model="daysToSkip" type="number"></b-form-input>
        </div>
       
    </b-container>
</template>

<script>
import { Currencies, DepositTypes} from '../data/data.js'
import { getAccounts, endBankDay } from "../services/accounts-api.js"
export default {
    name: "AccountList",
    data() {
        return {
            accounts: [],
            currencies: Currencies,
            depositTypes: DepositTypes,
            daysToSkip: 1,
        }
    },
    async mounted() {
      this.accounts = await this.GetAccounts();
    },
    methods: {
        GetAccounts: async () => {
            return await getAccounts();
        },
        EndBankDay: (daysToSkip) => {
            if (window.confirm("Подтвердите окончание банковского дня")) {
                endBankDay(daysToSkip).then(result => {
                    console.log(result)
                })
                this.$router.go()
            }
        }
    }
}
</script>

<style scoped>

.deposits-list-header {
    text-align: left;
}
.deposits-list-body {
    border: 1px solid lightgray;
    display: flex;
    flex-direction: column;
}
.deposits-list-body th, .deposits-list-body td {
    border: 1px solid lightgray;
    padding: 2px;
}
</style>