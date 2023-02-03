<template>
  <Navbar></Navbar>
  <div class="mt-16 text-white text-center">
    <h1>Pomyśnie zweryfikowano</h1>
    <h2>KeyMaster wita</h2>
    <router-link to="/">
      <v-btn class="danger mt-10 text-red text-xxl-caption" variant="outlined"
        >Przejdź do strony głównej
      </v-btn>
    </router-link>
  </div>
</template>

<script>
import { defineComponent } from "vue";
import Navbar from "@/components/Navbar.vue";

export default defineComponent({
  // eslint-disable-next-line vue/multi-word-component-names
  name: "verify",
  components: {
    Navbar,
  },
  mounted() {
    console.log(this.$route.query.userid);
    this.axios
      .put(
        "https://localhost:5001/api/Register/verify/" + this.$route.query.userid
      )
      .then((resposne) => {
        console.log(resposne);
      });
  },
});
</script>

<style scoped></style>
