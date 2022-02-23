<template>
    <b-container class="py-4 d-flex flex-column justify-content-center align-items-center">
        <h1>Терминал</h1>
        <div class="border shadow bg-lightgrey p-3 terminal-interface">
            <template v-if="step==1">
                <div class="d-flex flex-column justify-content-center align-items-start">
                    <h5>Введите карту</h5>
                    <b-form-input v-model="cardNumber" type="text"></b-form-input>
                    <h5 class="my-2">PIN</h5>
                    <b-form-input style="width: 200px" v-model="cardPin" type="password"></b-form-input>
                    <b-button :disabled="totalAttempts >= 3" @click="SubmitPIN" class="my-2 align-self-end">Подтвердить</b-button>
                </div>
            </template>
            <template v-else-if="step==2">
                <div class="d-flex flex-column justify-content-center align-items-center">
                    <h5>Выберите действие с картой</h5>
                    <b-button style="width: 200px" @click="step=3" class="my-2">Снять деньги</b-button>
                    <b-button style="width: 200px" @click="step=4" class="my-2">Состояние счёта</b-button>
                    <b-button style="width: 200px" @click="step=5" class="my-2">Провести платёж</b-button>
                    <b-button @click="RemoveCard" style="width: 200px" class="my-2">Забрать карту</b-button>
                </div>
            </template>
            <template v-else-if="step==3">
                <div class="d-flex flex-column justify-content-center align-items-center">
                    <h5>Введите сумму</h5>
                    <b-form-input style="width: 200px" v-model="withdrawalAmount" type="number" :min=0></b-form-input>
                    <b-button :disabled="withdrawalAmount <= 0" style="width: 200px" @click="SubmitWithdraw" class="my-2">Снять наличные</b-button>
                    <div class="my-5 d-flex flex-column justify-content-center align-items-center">
                        <b-button style="width: 200px" @click="step=2" class="my-2">Назад</b-button>
                        <b-button @click="RemoveCard" style="width: 200px" class="my-2">Забрать карту</b-button>
                    </div>
                    
                </div>
            </template>
            <template v-else-if="step==4">
                <div class="d-flex flex-column justify-content-center align-items-center">
                    <h5>Cостояние счёта № {{cardNumber}}</h5>
                    <b-form-input style="width: 200px" v-model="account.balance" type="number" readonly></b-form-input>
                    <div class="my-5 d-flex flex-column justify-content-center align-items-center">
                        <b-button style="width: 200px" @click="step=2" class="my-2">Назад</b-button>
                        <b-button @click="RemoveCard" style="width: 200px" class="my-2">Забрать карту</b-button>
                    </div>
                </div>
            </template>
            <template v-else-if="step==5">
                <div class="d-flex flex-column justify-content-center align-items-center">
                    <h5>Оплата услуг мобильного оператора</h5>
                    <b-form-select style="width: 500px" class="form-input" :options="phoneOperators" v-model="selectedPhoneOperator" :state="selectedPhoneOperator !== null"></b-form-select>
                    <b-form-input style="width: 500px" class="my-2" v-model="phoneNumber" type="text" placeholder="Введите номер телефона"></b-form-input>
                    <h5>Введите сумму</h5>
                    <b-form-input style="width: 200px" v-model="paymentAmount" type="number" :min=0></b-form-input>
                    <b-button :disabled="!phoneNumberState || paymentAmount <= 0 || selectedPhoneOperator == null" style="width: 200px" @click="SubmitPayment" class="my-2">Оплатить</b-button>
                    <div class="my-5 d-flex flex-column justify-content-center align-items-center">
                        <b-button style="width: 200px" @click="step=2" class="my-2">Назад</b-button>
                        <b-button @click="RemoveCard" style="width: 200px" class="my-2">Забрать карту</b-button>
                    </div>
                </div>
            </template>
            <template v-else-if="step==6">
                <div class="d-flex flex-column justify-content-center align-items-center">
                    <h5>Чек</h5>
                    <b-button style="width: 200px" @click="step=7" class="my-2">Печать</b-button>
                    <div class="my-5 d-flex flex-column justify-content-center align-items-center">
                        <b-button style="width: 200px" @click="step=2" class="my-2">Назад</b-button>
                        <b-button @click="RemoveCard" style="width: 200px" class="my-2">Забрать карту</b-button>
                    </div>
                </div>
            </template>
            <template v-else-if="step==7">
                <div class="d-flex flex-column justify-content-center align-items-center">
                    <div class="d-flex flex-column justify-content-center align-items-start">
                        <h5>Чек</h5>
                        <div>Дата транзакции: {{dateOfPayment}}</div>
                        <div>Тип транзакции: {{transactionType}}</div>
                        <div class="border-top py-2"></div>
                        <div class="border-top">Баланс до операции: {{prevBalance}}</div>
                        <div>Сумма транзакции: {{sum}}</div>
                        <div>Остаток по балансу: {{account.balance}}</div>
                    </div>
                    <div class="my-5 d-flex flex-column justify-content-center align-items-center">
                        <b-button style="width: 200px" @click="step=2" class="my-2">Назад</b-button>
                        <b-button @click="RemoveCard" style="width: 200px" class="my-2">Забрать карту</b-button>
                    </div>
                </div>
            </template>
        </div>
    </b-container>
</template>

<script>

import { pay, authAccount } from "../services/terminal-api.js"

export default {
    data() {
        return {
            step: 1,
            cardNumber: "",
            cardPin: "",
            account: { balance: 100 },
            totalAttempts: 0,

            withdrawalAmount: 0, 
            paymentAmount: 0,
            prevBalance: 0,

            dateOfPayment: null,
            transactionType: null,
            message: null,
            sum: null,
            
            phoneNumber: "",
            selectedPhoneOperator: null,
            phoneOperators: [
                {value: null, text: "Выберите оператора"},
                {value: 0, text: "A1"},
                {value: 1, text: "МТС"},
                {value: 2, text: "life:)"},
            ]
        }
    },
    computed: {
        phoneNumberState() {
            return /^[+]?[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\\./0-9]{8,8}$/.test(this.phoneNumber)
        }
    },
    methods: {
        SubmitPIN() {
            authAccount(this.cardNumber, this.cardPin).then(data => {
                alert(data.message)
                if (data.status == true) {
                    this.account = data.data
                    this.account.balance = this.account.balance + this.account.debit + this.account.credit
                    this.step = 2;
                }
                else {
                    this.totalAttempts++;
                }
            }).catch(err => {
                alert("Ошибка валидации")
                this.totalAttempts++;
                console.log(err)
            })
            
        },
        RemoveCard() {
            alert("Карта изъята")
            this.step = 1;
        },
        SubmitWithdraw() {
            if (window.confirm("Подтвердите вывод наличных")) {
                pay(this.account.id, this.withdrawalAmount).then(data => {
                    if (data.status == true) {
                        alert("Наличные выведены")
                        this.account = data.data
                        this.account.balance = this.account.balance + this.account.debit + this.account.credit
                        this.dateOfPayment = new Date().toDateString()
                        this.transactionType = "Вывод наличных средств"
                        this.sum = this.withdrawalAmount
                        this.prevBalance = this.account.balance - this.sum
                        this.step = 6;
                    }
                    else {
                        alert("Недостаточно средств на балансе")
                    }
                })
            }
        },
        SubmitPayment() {
            if (window.confirm("Подтвердите оплату")) {
                pay(this.account.id, this.paymentAmount).then(data => {
                    if (data.status == true) {
                        alert("Оплата совершена")
                        this.account = data.data
                        this.account.balance = this.account.balance + this.account.debit + this.account.credit
                        this.dateOfPayment = new Date().toDateString()
                        this.transactionType = "Оплата услуг мобильного оператора "
                        this.sum = this.paymentAmount
                        this.prevBalance = this.account.balance + this.sum
                        this.step = 6;
                    }
                    else {
                        alert("Недостаточно средств на балансе")
                    }
                })
            }
        }
    }
}
</script>

<style scoped>
    .bg-lightgrey {
        background: #f5f5f5;
    }
    .terminal-interface {
        width: 700px;
    }
    h3, h5 {
        text-align: start;
    }
</style>