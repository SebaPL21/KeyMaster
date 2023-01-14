<template>
  <v-app-bar elevation="6" class="navbar">
    <div class="nav">
      <router-link to="/"> <h1 class="logo">KeyMaster</h1></router-link>
      <div class="nav-links">
        <router-link to="/lesson"><h2>Lekcje</h2></router-link>
        <router-link to="/test"><h2>Test</h2></router-link>
      </div>
      <div class="nav-signs-buttons">
        <router-link to="/signup"
          ><button class="">Zaloguj się</button>
        </router-link>
        <button class="" @click="logout()">Wyloguj się</button>
      </div>
    </div>
  </v-app-bar>
</template>

<script lang="ts">
import "@/styles/style.scss";
import { useAuth0 } from "@auth0/auth0-vue";

export default {
  name: "NavbarComponent",
  setup() {
    const { loginWithRedirect, user, logout } = useAuth0();
    return {
      login: () => {
        loginWithRedirect();
        console.log("zalogowano" + user.value.name);
      },
      logout: () => {
        logout({ returnTo: window.location.origin });
        console.log("wylogowano");
      },
      user,
    };
  },
};
</script>
