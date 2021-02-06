<template>
  <!-- Modal Login -->
  <div class="modal fade" id="modal-login" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">{{$t('common.Login')}}</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div   @keydown.enter="Login">
            <div class="form-group row">
              <div class="col-sm-12">
                <input type="text" class="form-control" :placeholder="$t('common.Username')" v-model="username">
              </div>
            </div>
            <div class="form-group row">
              <div class="col-sm-12">
                <input type="password" class="form-control" :placeholder="$t('common.Password')" v-model="password">
              </div>
            </div>
          </div>
          
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">{{$t('common.Close')}}</button>
          <button type="button" class="btn btn-primary " @click="Login()"><i class="fa fa-sign-in"></i>&nbsp; {{$t('common.Login')}}</button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
  import axios from 'axios';
  export default {
    data() {
      return {
        username: '',
        password:''
      }
    },
    methods: {
      Login() {
        if (this.username == '') {
          NotifyDanger(this.langIndex("alert.Login_input_UserEmpty"));
          return;
        }
        if (this.password == '') {
          NotifyDanger(this.langIndex("alert.Login_input_PasswordEmpty"));
          return;
        }

        axios.post("/Auth/Login", { username: this.username, password: this.password }).then(res => {
          if (res.data != '') {
            this.$store.state.user = res.data;
     
            FadeLoadingBar();
            $("#modal-login").modal("hide");
            NotifySuccess(this.langIndex("alert.Login_NotiSuccess"));
          }
          else {
             NotifyDanger(this.langIndex("alert.Login_NotiDanger"));
          }
        }).catch(err => {
          NotifyDanger(this.langIndex("alert.Login_ErrorSysten"));
        })
      }
    }
  }
</script>
