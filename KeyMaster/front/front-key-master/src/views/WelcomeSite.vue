<template>
  <Navbar></Navbar>
  Witaj w aplikacji KeyMaster
  <Footer></Footer>
</template>

<script>
import Navbar from "@/components/Navbar.vue";
import Footer from "@/components/Footer.vue";
import {defineComponent} from "vue";

export default defineComponent( {
  components:{
    Navbar,
    Footer,
  },
}};
</script>

<style scoped></style>
