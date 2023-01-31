<template>
  <article>
    <div class="Sign-container" :class="{ 'sign-up-active': signUp }">
      <div class="overlay-container">
        <div class="overlay">
          <div class="overlay-left">
            <h2>Witaj z powrotem</h2>
            <p>Zaloguj się i sprawdź swoje postępy!</p>
            <button class="button invert" id="signIn" @click="signUp = !signUp">
              Zaloguj sie
            </button>
          </div>
          <div class="overlay-right">
            <h2>Witaj Przyjacielu</h2>
            <p>Podaj swoje dane i dołącz do nas</p>
            <button class="button invert" id="signUp" @click="signUp = !signUp">
              Rejestracja
            </button>
          </div>
        </div>
      </div>
      <form class="sign-up" @submit.prevent="submitRegistrationForm()">
        <h2>Stwóż login</h2>
        <div>Użyj swojego adresu email</div>
        <input type="text" v-model="nick" placeholder="Nick" />
        <input type="email" v-model="email" placeholder="Email" />
        <input type="password" v-model="password" placeholder="Hasło" />

        <button class="button" @click="sendEmail()">Rejestracja</button>
      </form>
      <form class="sign-in" @submit.prevent="submitLoginForm()">
        <h2>Zaloguj się</h2>
        <div>Podaj dane swojego konta</div>
        <input type="email" v-model="email" name="email" placeholder="Email" />
        <input
          type="password"
          v-model="password"
          name="password"
          placeholder="Hasło"
        />
        <a href="#">Zapomniałeś hasła?</a>
        <button class="button" type="submit">Zaloguj</button>
      </form>
    </div>
  </article>
</template>
<script lang="ts">
import "@/styles/style.scss";
import axios from "axios";
import "vue-router/dist/vue-router";
import router from "@/router";
import emailjs from "emailjs-com";

import { defineComponent } from "vue";

export default defineComponent({
  name: "SignupPage",
  data() {
    return {
      email: "",
      password: "",
      nick: "",
      signUp: false,
    };
  },
  components: {},
  methods: {
    submitLoginForm() {
      axios
        .post("https://localhost:5001/api/user/login", {
          email: this.email,
          password: this.password,
        })
        .then((response) => {
          console.log(response);
          console.log("asdasd");
          localStorage.setItem("token", response.data);
          router.push("/");
        })
        .catch((error) => {
          console.log(error);
        });
    },
    submitRegistrationForm() {
      axios
        .post("https://localhost:5001/api/Register/register", {
          nickname: this.nick,
          email: this.email,
          password: this.password,
        })
        .then((response) => {
          localStorage.setItem("token", response.data.token);
          router.push("/");
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    // sendEmail() {
    //   Email.send({
    //     Host: "sandbox.smtp.mailtrap.io",
    //     Username: "93e88b8394ec23",
    //     Password: "7e7d4338ccbcef",
    //     To: "recipient@example.com",
    //     From: "sender@example.com",
    //     Subject: "Test email",
    //     Body: "<html><h2>Header</h2><strong>Bold text</strong><br></br><em>Italic</em></html>",
    //   }).then((message) => alert(message));
    // },
    sendEmail() {
      try {
        console.log("dupa");
        emailjs.send(
          "service_okkj1vs",
          "template_cqt7rsp",
          {
            name: this.nick,
            email: this.email,
            message: "Weryfikacja konta <button> Zweryfikuj maila </button> ",
          },
          "k8R68iqRUP5AQf5tB"
        );
      } catch (error) {
        console.log({ error });
      }
    },
  },
});
</script>
