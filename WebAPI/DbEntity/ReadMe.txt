			/*
             * Entity framework self referencing loop detected.
             * */
            Configuration.ProxyCreationEnabled = false;//this is line to be added


			public HpmgCMSEntities()
            : base("name=HpmgCMSEntities")
        {
            /*
             * Entity framework self referencing loop detected.
             * */
            Configuration.ProxyCreationEnabled = false;//this is line to be added
        }
    