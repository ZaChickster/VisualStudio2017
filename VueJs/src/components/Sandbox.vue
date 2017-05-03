<template>
  <div class="sandbox" v-bind:title="bound">
    <h1>{{ message }}</h1>
  </div>
</template>

<script>
export default {
  name: 'sandbox',
  data () {
    return {
      message: 'Welcome to the Sandbox control.',
      bound: 'Generated at ' + new Date()
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: bold;
}
</style>
