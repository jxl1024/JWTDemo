<template>
    <div  class="content">
        <div style="margin: 20px;"></div>
        <el-form :label-position="labelPosition" label-width="80px" :model="formLabelAlign">
            <el-form-item label="用户名">
                <el-input v-model="formLabelAlign.UserCode" placeholder="请输入用户名"></el-input>
            </el-form-item>
            <el-form-item label="密  码">
                <el-input placeholder="请输入密码" v-model="formLabelAlign.Password" show-password></el-input>
            </el-form-item>
        </el-form>
        <div style="margin-top:30px;">
            <el-button type="primary" @click="loginHandle">登  录</el-button>
        </div>
    </div>
</template>

<script>
export default {
    data () {
        return {
            labelPosition: 'right',
            formLabelAlign: {
                UserCode: '',
                Password: ''
            }
        }
    },
    methods: {
        loginHandle () {
            // 发送post请求
            let url = 'http://localhost:5000/api/login'
            let params = { 'UserCode': this.formLabelAlign.UserCode, 'Password': this.formLabelAlign.Password }
            this.axios({
                method: 'post',
                url: url,
                data: params
            }).then(res => {
                alert('成功')
                console.log(res.data)
                // 把Token存储到sessionStorage中
                sessionStorage.setItem('Token', res.data.content)
                // 跳转到主页面
                this.$router.push('/home')
            })
        }
    }
}
</script>

<style scoped>
.content{
        width: 400px;
        height: 400px;
        margin: 0 auto;
        position: absolute;
        left:50%;
        top:50%;
        margin-left:-200px;
        margin-top:-200px;
}
</style>
