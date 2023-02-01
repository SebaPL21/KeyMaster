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
        <h2>Stwórz login</h2>
        <div>Użyj swojego adresu email</div>
        <input type="text" v-model="nick" placeholder="Nick" />
        <input type="email" v-model="email" placeholder="Email" />
        <input type="password" v-model="password" placeholder="Hasło" />

        <!--        <button class="button" @click="sendEmail()">Rejestracja</button>-->
        <button class="button">Rejestracja</button>
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
        <div v-if="error">
          <span class="text-red">Podano nie właściwe dane.</span>
        </div>
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
      error: false,
      userID: 0,
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
          this.error = true;
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
          this.userID = response.data.UserId;
          this.sendEmail();
          localStorage.setItem("token", response.data.token);
          router.push("/");
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    sendEmail() {
      try {
        emailjs.send(
          "service_okkj1vs",
          "template_cqt7rsp",
          {
            name: this.nick,
            email: this.email,
            message: "Weryfikacja konta",
            UserId: this.userID,
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
