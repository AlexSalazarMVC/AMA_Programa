import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthRouteModule } from './auth.route.module';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth/auth.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { SmtpConfigurationService } from '../../services/configuracion/smtp.service';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ConfirmationPasswordComponent } from './confirmation-password/confirmation-password.component';
import { AppModule } from '../../app.module';

@NgModule({ declarations: [
        LoginComponent,
        ResetPasswordComponent,
        ConfirmationPasswordComponent
    ],
    exports: [
        AuthRouteModule
    ], imports: [CommonModule,
        ReactiveFormsModule,
        FormsModule,
        // NgModule,
        AuthRouteModule], providers: [AuthService, SmtpConfigurationService, provideHttpClient(withInterceptorsFromDi())] })
export class AuthModule {}
