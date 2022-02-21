<template>
    <b-container class="credits-list my-4">
        <h3 class="credits-list-header">Активные Кредиты</h3>
        <div class="credits-list-body"> 
            <table>
                <tr>
                    <th>№</th>
                    <th>Идентификационный номер клиента</th>
                    <th>Тип</th>
                    <th>Cумма кредита</th>
                    <th>Погашено основной суммы</th>
                    <th>Непогашенные проценты</th>
                    <th>Валюта</th>
                    <th>Начало</th>
                    <th>Конец</th>
                    <th>Статус</th>
                    <th>Действия</th>
                </tr>
                <tr v-for="credit in credits" :key="credit.id">
                    <td>{{credit.id}}</td>
                    <td>{{credit.client.passportIdNumber}}</td>
                    <td>{{creditTypes.find(t => t.Id === credit.creditTypeId).Name}}</td>
                    <td>
                        <div>{{credit.creditAmount}}</div>
                    </td>
                    <td>
                        <div>{{credit.payedToDate}}</div>
                    </td>
                    <td>
                        <div>{{credit.percentAccount.balance}}</div>
                    </td>
                    <td>{{currencies.find(t => t.Id === credit.currencyId).Name}} </td>
                    <td>{{credit.startDate}}</td>
                    <td>{{credit.endDate}}</td>
                    <td>{{credit.status ? "Активен" : "Закрыт"}}</td>
                    <td><b-button v-if="credit.status && credit.pematureRepayment" @click="RevokeCredit(credit.id)">Отозвать</b-button></td>
                </tr>
            </table>
        </div>
    </b-container>
</template>

<script>
import { Currencies, CreditTypes} from '../data/data.js'
import { getCredits, revokeCredit } from "../services/credits-api.js"
export default {
    name: "CreditList",
    data() {
        return {
            credits: [],
            currencies: Currencies,
            creditTypes: CreditTypes,
        }
    },
    async mounted() {
      this.credits = await this.GetCredits();
    },
    methods: {
        GetCredits: async () => {
            return await getCredits();
        },
        RevokeCredit: (id) => {
            if (window.confirm("Подтвердите отзыв")) {
                revokeCredit(id)(result => {
                    alert(result)
                })
                this.$router.go()
            }
        }
    }
}
</script>

<style scoped>

.credits-list-header {
    text-align: left;
}
.credits-list-body {
    border: 1px solid lightgray;
    display: flex;
    flex-direction: column;
}
.credits-list-body th, .credits-list-body td {
    border: 1px solid lightgray;
    padding: 2px;
}
</style>