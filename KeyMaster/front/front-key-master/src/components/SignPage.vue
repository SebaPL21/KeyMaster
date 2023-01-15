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
      <form class="sign-up" action="#">
        <h2>Stwóż login</h2>
        <div>Użyj swojego adresu email</div>
        <input type="text" placeholder="Nick" />
        <input type="email" placeholder="Email" />
        <input type="password" placeholder="Hasło" />
        <button class="button">Rejestracja</button>
      </form>
      <form class="sign-in" @submit.prevent="submitForm()">
        <h2>Zaloguj się</h2>
        <div>Podaj dane swojego konta</div>
        <input v-model="email" name="email" placeholder="Email" />
        <input
          type="password"
          v-model="password"
          name="password"
          placeholder="Hasło"
        />
        <a href="#">Zapomniałeś hasła?</a>
        <button class="button" type="submit">Zaloguj</button>
        <button class="button" @click="login()">Zaloguj się</button>
      </form>
    </div>
  </article>
</template>
<script lang="ts">
import "@/styles/style.scss";
import { useAuth0 } from "@auth0/auth0-vue";
import axios from "axios";
import "vue-router/dist/vue-router";
import router from "@/router";
import { defineComponent } from "vue";
export default defineComponent({
  name: "SignupPage",
  data() {
    return {
      email: "",
      password: "",
      signUp: false,
    };
  },
  components: {},
  methods: {
    submitForm() {
      axios
        .post("https://localhost:5001/api/JWTToken/login", {
          email: this.email,
          password: this.password,
        })
        .then((response) => {
          localStorage.setItem("token", response.data.token);
          router.push("/");
          console.log(response);
          //this.$router.push("/");
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  // setup() {
  //   const { loginWithRedirect, user, logout } = useAuth0();
  //   return {
  //     login: () => {
  //       loginWithRedirect();
  //       console.log("zalogowano" + user.value.name);
  //     },
  //     logout: () => {
  //       logout({ returnTo: window.location.origin });
  //     },
  //     user,
  //   };
  // },
});
</script>
