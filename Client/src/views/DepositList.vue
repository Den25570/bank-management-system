<template>
    <b-container class="deposits-list my-4">
        <h3 class="deposits-list-header">Активные Депозиты</h3>
        <div class="deposits-list-body"> 
            <table>
                <tr>
                    <th>№</th>
                    <th>Идентификационный номер клиента</th>
                    <th>Тип</th>
                    <th>Cумма депозита</th>
                    <th>Проценты</th>
                    <th>Валюта</th>
                    <th>Начало</th>
                    <th>Конец</th>
                    <th>Статус</th>
                    <th>Действия</th>
                </tr>
                <tr v-for="deposit in deposits" :key="deposit.id">
                    <td>{{deposit.id}}</td>
                    <td>{{deposit.client.passportIdNumber}}</td>
                    <td>{{depositTypes.find(t => t.Id === deposit.depositTypeId).Name}}</td>
                    <td>
                        <div>{{deposit.depositAmount}}</div>
                    </td>
                    <td>
                        <div>{{deposit.percentAccount.balance}}</div>
                    </td>
                    <td>{{currencies.find(t => t.Id === deposit.currencyId).Name}} </td>
                    <td>{{deposit.startDate}}</td>
                    <td>{{deposit.endDate}}</td>
                    <td>{{deposit.status ? "Активен" : "Закрыт"}}</td>
                    <td><b-button v-if="deposit.status && deposit.depositTypeId === 1" @click="RevokeDeposit(deposit.id)">Отозвать</b-button></td>
                </tr>
            </table>
        </div>
    </b-container>
</template>

<script>
import { Currencies, DepositTypes} from '../data/data.js'
import { getDeposits, revokeDeposit } from "../services/deposits-api.js"
export default {
    name: "DepositList",
    data() {
        return {
            deposits: [],
            currencies: Currencies,
            depositTypes: DepositTypes,
        }
    },
    async mounted() {
      this.deposits = await this.GetDeposits();
    },
    methods: {
        GetDeposits: async () => {
            return await getDeposits();
        },
        RevokeDeposit: (id) => {
            if (window.confirm("Подтвердите отзыв")) {
                revokeDeposit(id).then(result => {
                    console.log(result)
                    this.$router.go()
                })
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