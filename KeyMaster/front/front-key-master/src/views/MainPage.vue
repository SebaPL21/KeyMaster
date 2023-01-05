<template>
   <Navbar></Navbar>     
   <Content></Content>
   <Footer></Footer>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import Navbar from "@/components/Navbar.vue";
import Content from "@/components/Content.vue";
import Footer from "@/components/Footer.vue";

@Options({
  components: {
    Navbar,
    Content,
    Footer,
  },
  })
export default class MainPage extends Vue {}
</script>
