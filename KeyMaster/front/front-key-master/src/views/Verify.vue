<template>
  <Navbar></Navbar>
  <div style="margin-top: 100px">POmyśnie zweryfikowano</div>
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
