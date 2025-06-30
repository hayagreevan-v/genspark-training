import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { specifiedUserGuard } from './specified-user-guard';

describe('specifiedUserGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => specifiedUserGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
