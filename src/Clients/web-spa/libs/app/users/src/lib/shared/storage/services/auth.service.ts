import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';

import { User } from '../../models/user.model';
import { JwtTokenService } from './jwt-token.service';
import { JwtToken } from '../../models/jwt-token.model';
import { UsersEndpointBuilder } from '../users-endpoint.builder';
// eslint-disable-next-line @nrwl/nx/enforce-module-boundaries
import { CookiesService } from '../../../../../../../shared/storage/src/lib/local/cookies.service';
// eslint-disable-next-line @nrwl/nx/enforce-module-boundaries
import { AbstractApiService } from '../../../../../../../shared/storage/src/lib/remote/abstract-api.service';
// eslint-disable-next-line @nrwl/nx/enforce-module-boundaries
import { EndpointBuilder } from '../../../../../../../shared/storage/src/lib/remote/endpoints/endpoint.builder';

@Injectable()
export class AuthService extends AbstractApiService {
  public constructor(
    http: HttpClient,
    @Inject(UsersEndpointBuilder) endpointBuilder: EndpointBuilder,
    @Inject(JwtTokenService) private tokenService: CookiesService<JwtToken>
  ) {
    super(http, endpointBuilder);
  }

  public verify(): Observable<User> {
    const tokenInCookies = this.tokenService.read();

    return tokenInCookies
      ? this.requestVerify(tokenInCookies)
      : throwError('Token is empty.');
  }

  public signIn(user: User): Observable<JwtToken> {
    const endpoint = this.endpointBuilder.withAction('SignIn').build();

    return this.http.post<JwtToken>(endpoint.url, user);
  }

  public signUp(user: User): Observable<JwtToken> {
    const endpoint = this.endpointBuilder.withAction('SignUp').build();

    return this.http.post<JwtToken>(endpoint.url, user);
  }

  ///////////////////////////////////////////////////////////////////////////
  // Helpers
  ///////////////////////////////////////////////////////////////////////////

  private requestVerify(token: JwtToken): Observable<User> {
    const endpoint = this.endpointBuilder.withAction('Verify').build();

    return this.http.post<User>(
      endpoint.url,
      token,
      this.withAuthorization(token)
    );
  }
}
