<template>
  <div>

    <nav class="navbar navbar-expand-lg navbar-light  " id="nav-menu">
      <div class="container">
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarNav">
          <div class="logo mr-4">
            <router-link to="/"><img class="img-fluid" src="/Images/logo/syf1.png" /></router-link>
          </div>
          <div class="nav-left pull-left">
            <ul class="navbar-nav">


              <router-link tag="li"
                           to="/"
                           class="nav-item"
                           active-class="active"
                           exact>
                <a class="nav-link" href="#">{{$t('common.Home')}}</a>

              </router-link>

              <router-link tag="li"
                           to="/thiet-bi"
                           class="nav-item"
                           active-class="active"
                           exact v-if="user!=null">
                <a class="nav-link" href="#"> {{$t('common.Device')}}</a>

              </router-link>
              <router-link tag="li"
                           to="/bo-phan"
                           class="nav-item"
                           active-class="active"
                           exact v-if="user!=null">
                <a class="nav-link" href="#">{{$t('common.Depart')}}</a>


              </router-link>
              <router-link tag="li"
                           to="/lich-su"
                           class="nav-item"
                           active-class="active"
                           exact>
                <a class="nav-link" href="#">{{$t('common.History')}}</a>


              </router-link>


            </ul>
          </div>

          <div class="nav-right pull-right">
            <ul>
              <li class="system-time">
                <a href="#" id="clock">
                  10:10:10
                </a>
              </li>
              <li class="login-box">

                <div class="account-wrap">
                  <a href="#">
                    <img class="img-fluid" src="/Images/account.png" />
                  </a>
                  <a href="#" data-toggle="modal" data-target="#modal-login" class="" v-show="user==null">{{$t('common.Login')}}</a>
                  <template v-if="user!=null">
                    <a href="#" class="dropdown-toggle user-confirm" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                      {{$t('common.Hello')}} ! {{user.FullName}}
                    </a>
                    <div class="dropdown-menu dropdown-menu-right pull-right">

                      <a class="dropdown-item" @click="Logout()">
                        <i class="fa fa-sign-out"> </i>
                        {{$t('common.Logout')}}
                      </a>
                    </div>
                  </template>

                </div>
              </li>
              <li class="language">
                <a href="#" data-toggle="tooltip" data-placement="bottom" title="Taiwanese" @click="setLanguage('zh')">
                  <img src="/Images/language/tw.png" />
                </a>
                <a href="#" data-toggle="tooltip" data-placement="bottom" title="English" @click="setLanguage('en')">
                  <img src="/Images/language/us.png" />
                </a>
                <a href="#" data-toggle="tooltip" data-placement="bottom" title="Vietnamese" @click="setLanguage('vn')">
                  <img src="/Images/language/vn.png" />
                </a>
             
              </li>
            </ul>
          </div>
        </div>
      </div>
    </nav>

    <section id="main">
      <!-- =======router view========= -->
      <router-view></router-view>

    </section>

    <section id="footer">
      <!-- Login Component -->
      <login-component></login-component>
      <div class="container">
        <div class="info">
          <div class="row">
            <div class="col-12">
              <div class="copyright">
                <i class="fa fa-registered"></i>
                <i class="fa fa-copyright"></i>
                <span>2019 - SHC Group</span>
              </div>
              <div class="box">
                Powered by IT-SHC
                <img src="Images/8.gif" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
  import LoginComponent from './Login/LoginComponent';

  export default {
    components: {
      LoginComponent
    },
    data() {
      return {
        isChek: false
      }
    },
    computed: {
      user() {
        return this.$store.state.user;
      },

    },
    methods: {
      Logout() {
        this.$cookies.remove("UserCookie");
        this.$store.state.user = null;
        this.$router.push('/');
      },
      checkCookie() {
        var userCookie = this.$cookies.get("UserCookie");
        if (userCookie != null && userCookie != '') {
          this.$store.state.user = userCookie;

        }
        var locateCookie = this.$cookies.get("locate");
        if (locateCookie != '' && locateCookie != null) {
          this.$i18n.locale = locateCookie;
        }
      },
      setLanguage(lang) {
        this.$cookies.set("locate", lang);
        //this.$store.dispatch('setLang', lang);
        window.location.reload();
      }
    },
    updated() {
      document.title = this.$route.meta.title;
    },
    created() {
      this.checkCookie();
    }
  }
</script>


