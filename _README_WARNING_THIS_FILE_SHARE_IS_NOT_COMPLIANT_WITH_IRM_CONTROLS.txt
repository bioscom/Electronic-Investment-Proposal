[THIS FILE IS PLACED ON THIS FILE SHARE on April 27th, 2015]

IMPORTANT! 

This file share is identified by the Open Share Remediation (OSR) team as a non-compliant (open) file share for which no proper Risk Acceptance was put in place during the previous two years.

Please make sure this share is:
1. made compliant by yourself or by your application support teams (e.g. by not longer sharing this folder or by modifying the file shares permissions) OR
2. contact the Open Share Remediation Team (ITSO -IRM Open Share Remediation SITI-ITGF/ECI) (ITSO-IRM-Open-Share-Remediation@shell.com) to assist with the remediation or the risk acceptance of this issue.

A file share/shared folder is considered to be open (non-compliant) when its contents are accessible for every single IT user in our global IT-infra due to ..

1. One (or more) of the following generic NTFS user security groups being used on the share’s top level:
A) "everyone" (Everyone),
B) “authenticated users” (Authenticated Users),
C) “NT AUTHORITY\NETWORK” (Network),
D) “BUILTIN\Users” (Users),
E) “BUILTIN\guests” (Guests) and
F) 'Domain Users' security groups (e.g. “EUROPE\Domain Users”, “AMERICAS\Domain Users”, “ASIA-PAC\Domain Users”, etc.) 

2. Usage of the (re-enabled) servers guest account

More information can be found in the approved OneIT control with regards to file shares (GC.056) ..
https://eu001-sp.shell.com/sites/AAAAA0128/VCP/Controls_community/PYTHON_REPORTS/data/GC.056.2013.html

and in the supporting control procedure ..
https://eu001-sp.shell.com/sites/AAAAA0128/ITSIRM/Compl/PublicDocuments/Remediate Share Access/File Shares - CPS for GC.056 (final, july 2013).pdf

as well as in the following document which provides explicit guidance (via Do's and Don'ts) on how to properly restrict access to file shares:
https://eu001-sp.shell.com/sites/AAAAA0128/ITSIRM/Compl/PublicDocuments/Remediate Share Access/File Sharing within Shell - Do's and Don'ts 20131112.pptx

With regards,

The Open Share Remediation Team
ITSO -IRM Open Share Remediation SITI-ITGF/ECI
ITSO-IRM-Open-Share-Remediation@shell.com
