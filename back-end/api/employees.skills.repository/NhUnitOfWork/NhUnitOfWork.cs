﻿using System;
using employee.skill.common.infrastructure.Exceptions.Repositories;
using employee.skill.common.infrastructure.UnitOfWorks;
using NHibernate;

namespace employees.skills.repository.NhUnitOfWork;

public class NhUnitOfWork : IUnitOfWork
{
    private readonly ISession _session;

    public NhUnitOfWork(ISession session)
    {
        _session = session;
    }

    public bool TransactionHandled { get; private set; }
    public bool SessionClosed { get; private set; }

    public void Commit()
    {
        using ITransaction transaction = _session?.BeginTransaction();
        try
        {
            if (_session == null) return;
            if (!_session.Transaction!.IsActive) return;

            _session.Flush();
            transaction?.Commit();
            TransactionHandled = true;
        }
        catch (Exception ex)
        {
            transaction?.Rollback();
            TransactionHandled = false;
            throw new NHibernateSessionTransactionFailedException(ex.Message);
        }
    }

    public void Close()
    {
        _session.Close();
        _session.Dispose();
        SessionClosed = true;
    }
}