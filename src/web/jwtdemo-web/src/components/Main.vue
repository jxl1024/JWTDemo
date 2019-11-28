<template>
    <div>
         <el-button style="margin-left: -1250px;margin-top:30px;" type="success" @click="addUserHandle">新增用户</el-button>
         <div style="margin-top:20px;">
                <el-table
                    :data="userList"
                    border
                    style="width: 100%">
                    <el-table-column
                    prop="userCode"
                    label="用户编码"
                    width="180">
                    </el-table-column>
                    <el-table-column
                    prop="userName"
                    label="用户姓名"
                    width="180">
                    </el-table-column>
                    <el-table-column
                    prop="age"
                    label="年龄">
                    </el-table-column>
                </el-table>
         </div>
    </div>
</template>

<script>
export default {
    data () {
        return {
            userList: []
        }
    },
    methods: {
        // 获取用户信息
        initData () {
            let url = 'http://localhost:3000/api/user'
            this.axios({
                method: 'get',
                url: url,
                headers: {
                    // 把Token信息添加到HTTP请求的Header中
                    Authorization: 'bearer ' + sessionStorage.getItem('Token')
                }
            }).then(res => {
                this.userList = res.data
            }).catch(error => {
                alert('status:' + error.response.status)
                if (error.response.status === 401) {
                    alert('Token已过期，请重新登录')
                    // 跳转到登录页面
                    window.location.href = '/'
                }
            })
        },
        // 新增用户信息
        addUserHandle () {
            let url = 'http://localhost:3000/api/user'
            this.axios({
                method: 'post',
                url: url,
                headers: {
                    Authorization: 'bearer ' + sessionStorage.getItem('Token')
                }
            }).then(res => {
                alert('创建成功')
                // 重新加载数据
                this.initData()
            }).catch(error => {
                alert('status:' + error.response.status)
                if (error.response.status === 401) {
                    alert('Token已过期，请重新登录')
                    // 跳转到登录页面
                    window.location.href = '/'
                }
            })
        }
    },
    mounted () {
        this.initData()
    }
}
</script>
