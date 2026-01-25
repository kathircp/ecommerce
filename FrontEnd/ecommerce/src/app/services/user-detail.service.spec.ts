import { TestBed } from '@angular/core/testing';

import { UserDetailServiceService } from './user-detail.service.service';

describe('UserDetailServiceService', () => {
  let service: UserDetailServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserDetailServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
