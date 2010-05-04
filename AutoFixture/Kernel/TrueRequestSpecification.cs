﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.AutoFixture.Kernel
{
    /// <summary>
    /// A <see cref="IRequestSpecification"/> that is always <see langword="true"/>.
    /// </summary>
    public class TrueRequestSpecification : IRequestSpecification
    {
        #region IRequestSpecification Members

        /// <summary>
        /// Evaluates a request for a specimen.
        /// </summary>
        /// <param name="request">The specimen request.</param>
        /// <returns>
        /// <see langword="true"/>.
        /// </returns>
        public bool IsSatisfiedBy(object request)
        {
            return true;
        }

        #endregion
    }
}
