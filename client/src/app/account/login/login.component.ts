import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
loginForm: FormGroup;
returnUrl: string;
  constructor(private accountService: AccountService, private route: Router, private activateRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.returnUrl = this.activateRoute.snapshot.queryParams.returnUrl || '/shop';
    this.createLoginForm();
  }

  createLoginForm(){
    this.loginForm = new FormGroup({
      email: new FormControl('',[Validators.required,Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
      password : new FormControl('',Validators.required)
    }
    );
  }
  onSubmit(){
    this.accountService.login(this.loginForm.value).subscribe(() => {
      this.route.navigateByUrl(this.returnUrl);
    }, error => {
      console.log(error)
    });
  }
}
